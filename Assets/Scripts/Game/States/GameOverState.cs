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
            UIManager.Instance.ShowPanel<GameOverPanel>();
        }

        public override void OnStateCheckTransition()
        {
            base.OnStateCheckTransition();
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                this.Transfer("BACK_TO_MAIN");
            }
        }
    }
}
