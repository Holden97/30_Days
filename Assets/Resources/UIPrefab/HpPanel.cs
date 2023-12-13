using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class HpPanel : BaseUI
    {
        public Slider hp;
        private GameObject player;

        private Health playerHp;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHp = player.GetComponent<Health>();
        }

        private void Update()
        {
            hp.value = playerHp.curHp / playerHp.maxHp;
        }
    }
}
