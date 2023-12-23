﻿using BehaviorDesigner.Runtime;
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
        public GamingState(string stateName) : base(stateName)
        {
            CurWaveNo = 0;
        }

        public override void OnStateStart()
        {
            CurWaveNo++;
            var pi = GameObject.FindObjectOfType<PlayerInput>();
            UIManager.Instance.ShowPanel<DamageInfoPanel>();
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
                player = playerGo.GetComponentInChildren<Health>();
                playerPicker = playerGo.GetComponent<PlayerPicker>();
            }
            UIManager.Instance.HideAll();
            UIManager.Instance.ShowPanel<GamingInfoPanel>();

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

            //monsterTimer = new Timer(2, "生成怪物", onComplete: () =>
            //  {
            //      var go = ObjectPoolManager.Instance.GetNextObject("怪物");
            //      go.GetComponentInChildren<Health>().ResetHealth();
            //      go.GetComponentInChildren<BehaviorTree>().EnableBehavior();
            //      go.transform.SetPositionAndRotation(new Vector3(Random.Range(-10, 10f), Random.Range(-10, 10f), -1), Quaternion.identity);

            //  }, isLoop: true);
            //monsterTimer.Register();

            //测试临时添加武器
            var curWeapon = GameManager.Instance.GetWeaponData("砍刀");
            playerPicker.SetupWeapon(curWeapon);

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
            UIManager.Instance.Hide<DamageInfoPanel>();
        }
    }
}
