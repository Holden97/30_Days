using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource;
using CommonBase;
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
        public static int MaxWaveNo = 6;
        private Timer monsterTimer;
        private float lastCursorMoveTime;
        private Vector3 lastMousePos;
        public GamingState(string stateName) : base(stateName)
        {
            CurWaveNo = 0;
        }

        public override void OnStateStart()
        {
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

            TimeLeft = GameManager.Instance.waveDuration;
            base.OnStateStart();
            new Timer(TimeLeft, onComplete: () => waveOver = true).Register();
            var playerGo = GameObject.FindGameObjectWithTag("Player");
            if (playerGo != null)
            {
                player = playerGo.GetComponentInChildren<Health>();
                playerPicker = playerGo.GetComponent<PlayerPicker>();
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
            if (!ObjectPoolManager.Instance.Contains("子弹"))
            {
                ObjectPoolManager.Instance.CreatePool("子弹", GameManager.Instance.BulletPrefab, 200);
            }
            if (!ObjectPoolManager.Instance.Contains("金币"))
            {
                ObjectPoolManager.Instance.CreatePool("金币", GameManager.Instance.CoinPrefab, 200);
            }

            monsterTimer = new Timer(2, "生成怪物", onComplete: () =>
              {
                  var go = ObjectPoolManager.Instance.GetNextObject("怪物");
                  go.GetComponentInChildren<Health>().ResetHealth();
                  go.GetComponentInChildren<BehaviorTree>().EnableBehavior();
                  //go.GetComponentInChildren<Animator>().Play("Trainee_idle");
                  go.transform.SetPositionAndRotation(new Vector3(Random.Range(-10, 10f), Random.Range(-10, 10f), -1), Quaternion.identity);
                  go.GetComponent<Character>().Init();
                  GameManager.Instance.AddCharacter(go.GetComponent<Character>());
              }, isLoop: true);
            monsterTimer.Register();

            //UI
            UIManager.Instance.ShowPanel<GamingInfoPanel>();
            UIManager.Instance.ShowPanel<DamageInfoPanel>();
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
            if (waveOver && CurWaveNo < MaxWaveNo)
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
                Debug.Log("lastMousePos:" + lastMousePos);
                Debug.Log("Input.mousePosition:" + Input.mousePosition);
                lastCursorMoveTime = Time.time;
            }
            Cursor.visible = Time.time - lastCursorMoveTime < 1;
            TimerManager.Instance.Tick();
            TimeLeft -= Time.deltaTime;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            waveOver = false;
            monsterTimer.Unregister();
            UIManager.Instance.Hide<DamageInfoPanel>();
        }
    }
}
