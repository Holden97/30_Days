﻿using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OfficeWar
{
    public class PrepareState : BaseState
    {
        public PrepareState(string stateName) : base(stateName)
        {
        }

        public override void OnStateStart()
        {
            base.OnStateStart();
            SceneManager.LoadScene("MainMenu");
            UIManager.Instance.ShowPanel<WelcomePanel>();
        }

        public override void OnStateCheckTransition()
        {
            base.OnStateCheckTransition();
            if (SceneManager.GetActiveScene().name == "Battle")
            {
                this.Transfer("START_GAME");
            }
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            UIManager.Instance.Hide<WelcomePanel>();
        }
    }
}
