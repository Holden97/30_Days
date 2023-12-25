using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OfficeWar
{
    public class GameOverState : BaseState
    {
        public GameOverState(string stateName) : base(stateName)
        {
        }

        public override void OnStateStart()
        {
            base.OnStateStart();
            Cursor.visible = true;
            UIManager.Instance.ShowPanel<GameOverPanel>();
            var pi = GameObject.FindObjectOfType<PlayerInput>();
            if (pi != null)
            {
                pi.enabled = false;
            }
        }

        public override void OnStateCheckTransition()
        {
            base.OnStateCheckTransition();
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                this.Transfer("BACK_TO_MAIN");
            }
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            UIManager.Instance.HideAll();
            ObjectPoolManager.Instance.PutbackAll("怪物");
            ObjectPoolManager.Instance.PutbackAll("子弹");
            ObjectPoolManager.Instance.PutbackAll("金币");
            GameManager.Instance.player = null;
        }
    }
}
