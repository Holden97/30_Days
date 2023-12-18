using BehaviorDesigner.Runtime;
using CommonBase;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Timer = CommonBase.Timer;

namespace OfficeWar
{
    public class GamingState : BaseState
    {
        private Health player;
        public GamingState(string stateName) : base(stateName)
        {
        }

        public override void OnStateStart()
        {
            base.OnStateStart();
            var playerGo = GameObject.FindGameObjectWithTag("Player");
            if (playerGo != null)
            {
                player = playerGo.GetComponent<Health>();
            }
            UIManager.Instance.HideAll();
            UIManager.Instance.ShowPanel<HpPanel>();

            ObjectPoolManager.Instance.CreatePool(100, GameManager.Instance.MonsterPrefab, "怪物");
            ObjectPoolManager.Instance.CreatePool(200, GameManager.Instance.BulletPrefab, "子弹");

            new Timer(2, "生成怪物", onComplete: () =>
            {
                var go = ObjectPoolManager.Instance.GetNextObject("怪物");
                go.GetComponent<Health>().ResetHealth();
                go.GetComponent<BehaviorTree>().EnableBehavior();
                go.transform.SetPositionAndRotation(new Vector3(Random.Range(-10, 10f), Random.Range(-10, 10f), -1), Quaternion.identity);

            }, isLoop: true).Register();
            UIManager.Instance.ShowPanel<HpPanel>();
        }

        public override void OnStateCheckTransition()
        {
            base.OnStateCheckTransition();
            if (player == null || !player.IsAlive)
            {
                this.Transfer("GAME_OVER");
            }
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            TimerManager.Instance.Tick();
        }
    }
}
