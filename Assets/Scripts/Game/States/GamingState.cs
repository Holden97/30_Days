using BehaviorDesigner.Runtime;
using CommonBase;
using UnityEngine;
using Timer = CommonBase.Timer;

namespace OfficeWar
{
    public class GamingState : BaseState
    {
        private Health player;
        private bool waveOver;
        public static float TimeLeft;
        public static int CurWaveNo;
        public static int MaxWaveNo = 6;
        private Timer monsterTimer;
        public GamingState(string stateName) : base(stateName)
        {
            CurWaveNo = 0;
        }

        public override void OnStateStart()
        {
            CurWaveNo++;
            var pi = GameObject.FindObjectOfType<PlayerInput>();
            GameManager.Instance.player = GameObject.FindObjectOfType<PlayerPicker>();
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
                player = playerGo.GetComponent<Health>();
            }
            UIManager.Instance.HideAll();
            UIManager.Instance.ShowPanel<GamingInfoPanel>();

            if (!ObjectPoolManager.Instance.Contains("怪物"))
            {
                ObjectPoolManager.Instance.CreatePool(100, GameManager.Instance.MonsterPrefab, "怪物");
            }
            if (!ObjectPoolManager.Instance.Contains("子弹"))
            {
                ObjectPoolManager.Instance.CreatePool(200, GameManager.Instance.BulletPrefab, "子弹");
            }
            if (!ObjectPoolManager.Instance.Contains("金币"))
            {
                ObjectPoolManager.Instance.CreatePool(200, GameManager.Instance.CoinPrefab, "金币");
            }

            monsterTimer = new Timer(2, "生成怪物", onComplete: () =>
              {
                  var go = ObjectPoolManager.Instance.GetNextObject("怪物");
                  go.GetComponent<Health>().ResetHealth();
                  go.GetComponent<BehaviorTree>().EnableBehavior();
                  go.transform.SetPositionAndRotation(new Vector3(Random.Range(-10, 10f), Random.Range(-10, 10f), -1), Quaternion.identity);

              }, isLoop: true);
            monsterTimer.Register();
            UIManager.Instance.ShowPanel<GamingInfoPanel>();
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
            TimerManager.Instance.Tick();
            TimeLeft -= Time.deltaTime;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            waveOver = false;
            monsterTimer.Unregister();
        }
    }
}
