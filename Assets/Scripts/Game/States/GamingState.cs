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
        public static float WaveDuration = 10;
        public static int CurWave;
        public GamingState(string stateName) : base(stateName)
        {
        }

        public override void OnStateStart()
        {
            var pi = GameObject.FindObjectOfType<PlayerInput>();
            if (pi != null)
            {
                pi.enabled = true;
            }

            TimeLeft = WaveDuration;
            base.OnStateStart();
            new Timer(WaveDuration, onComplete: () => waveOver = true).Register();
            var playerGo = GameObject.FindGameObjectWithTag("Player");
            if (playerGo != null)
            {
                player = playerGo.GetComponent<Health>();
            }
            UIManager.Instance.HideAll();
            UIManager.Instance.ShowPanel<GamingInfoPanel>();

            ObjectPoolManager.Instance.CreatePool(100, GameManager.Instance.MonsterPrefab, "怪物");
            ObjectPoolManager.Instance.CreatePool(200, GameManager.Instance.BulletPrefab, "子弹");

            new Timer(2, "生成怪物", onComplete: () =>
            {
                var go = ObjectPoolManager.Instance.GetNextObject("怪物");
                go.GetComponent<Health>().ResetHealth();
                go.GetComponent<BehaviorTree>().EnableBehavior();
                go.transform.SetPositionAndRotation(new Vector3(Random.Range(-10, 10f), Random.Range(-10, 10f), -1), Quaternion.identity);

            }, isLoop: true).Register();
            UIManager.Instance.ShowPanel<GamingInfoPanel>();
        }

        public override void OnStateCheckTransition()
        {
            base.OnStateCheckTransition();
            if (player == null || !player.IsAlive || waveOver)
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
        }
    }
}
