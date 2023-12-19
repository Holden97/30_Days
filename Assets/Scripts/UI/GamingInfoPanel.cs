using CommonBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class GamingInfoPanel : BaseUI
    {
        public Slider hp;
        private GameObject player;
        private Health playerHp;
        public TMP_Text timer;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHp = player.GetComponent<Health>();
        }

        private void Update()
        {
            hp.value = playerHp.curHp / playerHp.maxHp;

            var timerLeftShow = (int)Mathf.Max(0, GamingState.TimeLeft);
            timer.text = timerLeftShow.ToString();
        }
    }
}
