using CommonBase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OfficeWar
{
    public class CharacterInfoPanel : BaseUI
    {
        public CommonList playerPropsList;
        public CommonList playerWeaponsList;
        public TMP_Text coinsText;

        public override void UpdateView(object o)
        {
            var s = o as PlayerPicker;
            if (s != null)
            {
                this.coinsText.text = s.coinsCount.ToString();
                this.playerWeaponsList.BindData(s.weapons);
                this.playerPropsList.BindData(s.props);
            }
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
