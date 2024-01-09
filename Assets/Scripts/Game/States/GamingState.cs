using CommonBase;
using System.Collections;
using UnityEngine;
using Timer = CommonBase.Timer;

namespace OfficeWar
{
    public class GamingState : BaseState
    {
        private Health player;
        private PlayerPicker playerPicker;
        private bool waveOver;
        public static float TimeLeft;
        public static int CurWaveNo;
        public int MaxWaveNo;
        private Timer monsterTimer;
        private float lastCursorMoveTime;
        private Vector3 lastMousePos;
        public CompositeCollider2D tc2d;
        public static float MonsterZ = -1;
        private bool collectionFlag = false;
        public GamingState(string stateName) : base(stateName)
        {
            CurWaveNo = 0;
            MaxWaveNo = GameManager.Instance.waveCount;
        }

        public override void OnStateStart()
        {
            tc2d = GameObject.FindObjectOfType<CompositeCollider2D>();
            UIManager.Instance.HideAll();
            GameManager.Instance.player = GameObject.FindObjectOfType<PlayerPicker>();
            GameManager.Instance.ClearEnemies();
            CurWaveNo++;
            Time.timeScale = 1.0f;

            var pi = GameObject.FindObjectOfType<PlayerInput>();
            if (pi != null)
            {
                pi.enabled = true;
            }

            TimeLeft = GameManager.Instance.GetCurWaveDuration(CurWaveNo);
            base.OnStateStart();
            new Timer(TimeLeft, onComplete: () => waveOver = true).Register();
            var playerGo = GameObject.FindGameObjectWithTag("Player");
            if (playerGo != null)
            {
                player = playerGo.GetComponentInChildren<Health>();
                playerPicker = playerGo.GetComponentInChildren<PlayerPicker>();
                GameManager.Instance.AddCharacter(playerGo.GetComponent<Character>());
            }

            if (CurWaveNo == 1)
            {
                //装备初始武器
                SetUpWeapon(GameManager.Instance.gameData.initialWeaponData.name);
            }

            if (!ObjectPoolManager.Instance.Contains("怪物"))
            {
                ObjectPoolManager.Instance.CreatePool("怪物", GameManager.Instance.MonsterPrefab, 100);
            }
            if (!ObjectPoolManager.Instance.Contains("预警"))
            {
                ObjectPoolManager.Instance.CreatePool("预警", GameManager.Instance.AlertPrefab, 30);
            }
            if (!ObjectPoolManager.Instance.Contains("子弹"))
            {
                ObjectPoolManager.Instance.CreatePool("子弹", GameManager.Instance.BulletPrefab, 200);
            }
            if (!ObjectPoolManager.Instance.Contains("金币"))
            {
                ObjectPoolManager.Instance.CreatePool("金币", GameManager.Instance.CoinPrefab, 200);
            }

            monsterTimer = new Timer(10, "生成怪物",
                OnStart: () =>
                {
                    GenerateGroup(10, Random.insideUnitCircle * 10, GameManager.Instance.generateSpan);
                },
                onComplete: () =>
              {
                  GenerateGroup(10, Random.insideUnitCircle * 10, GameManager.Instance.generateSpan);
              }, isLoop: true);
            monsterTimer.Register();

            //UI
            UIManager.Instance.ShowPanel<GamingInfoPanel>();
            UIManager.Instance.ShowPanel<DamageInfoPanel>();
        }

        public void GenerateGroup(int count, Vector2 center, float generateSpan)
        {
            var mapRect
                = new Rect(tc2d.bounds.min + new Vector3(1, 1, 0),
                tc2d.bounds.max - tc2d.bounds.min - new Vector3(2, 2, 0));
            for (int i = 0; i < count; i++)
            {
                GameManager.Instance.StartCoroutine(GenerateSingle(center, mapRect, Random.Range(0, generateSpan)));
            }
        }

        private IEnumerator GenerateSingle(Vector2 center, Rect bounds, float delay)
        {
            yield return new WaitForSeconds(delay);
            var go = ObjectPoolManager.Instance.GetNextObject("预警");
            var initPos = center.To3() + new Vector3(Random.Range(-5, 5f), Random.Range(-5, 5f), MonsterZ);
            if (!bounds.Contains(initPos))
            {
                initPos.x = Mathf.Clamp(initPos.x, bounds.min.x, bounds.max.x);
                initPos.y = Mathf.Clamp(initPos.y, bounds.min.y, bounds.max.y);
            }
            go.transform.SetPositionAndRotation(initPos, Quaternion.identity);
        }

        public IEnumerator GenerateRealMonster()
        {
            var alert = ObjectPoolManager.Instance.GetNextObject("预警");
            yield return new WaitForSeconds(1);
        }

        public void SetUpWeapon(string name)
        {
            playerPicker.SetupWeapon(GameManager.Instance.GetWeaponData(name));
        }

        public override void OnStateCheckTransition()
        {
            base.OnStateCheckTransition();
            if (player == null || !player.IsAlive)
            {
                this.Transfer("GAME_OVER");
            }
            if (waveOver && CurWaveNo < MaxWaveNo && ObjectPoolManager.Instance.GetAllActiveObjectsCount("金币") <= 0)
            {
                this.Transfer("GO_SHOPPING");
            }
            else if (waveOver && CurWaveNo >= MaxWaveNo)
            {
                this.Transfer("GAME_OVER");
            }
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            if (Input.mousePosition != lastMousePos)
            {
                lastMousePos = Input.mousePosition;
                lastCursorMoveTime = Time.time;
            }
            Cursor.visible = Time.time - lastCursorMoveTime < 1;
            TimerManager.Instance.Tick();
            TimeLeft -= Time.deltaTime;
            if (waveOver && !collectionFlag)
            {
                CollectAllCoins();
                monsterTimer.Unregister();
                collectionFlag = true;
            }
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            waveOver = false;
            monsterTimer.Unregister();
            UIManager.Instance.Hide<DamageInfoPanel>();

            collectionFlag = false;
        }

        private void CollectAllCoins()
        {
            var gameInfoPanel = UIManager.Instance.Get<GamingInfoPanel>();
            var coinsQuantityPos = Camera.main.ScreenToWorldPoint(gameInfoPanel.coinsCount.transform.position);
            coinsQuantityPos = new Vector3(coinsQuantityPos.x, coinsQuantityPos.y, MonsterZ);

            foreach (var item in ObjectPoolManager.Instance.GetAllActiveObjects("金币"))
            {
                GameManager.Instance.StartCoroutine(CollectSingleCoin(item, coinsQuantityPos));
            }
        }

        private IEnumerator CollectSingleCoin(GameObject coin, Vector3 targetPos)
        {
            float timer = 0;
            var dir = (targetPos - coin.transform.position).normalized;
            while (Vector3.Distance(targetPos, coin.transform.position) > .3f)
            {
                timer += Time.fixedDeltaTime;
                coin.transform.position += dir * Time.fixedDeltaTime * GameManager.Instance.waves.coinAttractionCurve.Evaluate(timer);
                yield return null;
            }
            ObjectPoolManager.Instance.Putback("金币", coin);
            playerPicker.coinsCount++;
        }
    }
}
