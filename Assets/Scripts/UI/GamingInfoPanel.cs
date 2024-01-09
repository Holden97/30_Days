using CommonBase;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class GamingInfoPanel : BaseUI
    {
        public Slider hpPercent;
        public Slider expPercent;

        public TMP_Text hp;
        public TMP_Text level;
        public TMP_Text levelProgress;

        private GameObject player;
        private PlayerPicker picker;
        private Health playerHp;
        public TMP_Text timer;
        public TMP_Text waveNo;
        public TMP_Text coinsCount;

        private StringBuilder hpSb;
        private StringBuilder expSb;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHp = player.GetComponentInChildren<Health>();
            picker = playerHp.GetComponentInChildren<PlayerPicker>();
            hpSb = new StringBuilder();
            expSb = new StringBuilder();
        }

        private void Update()
        {
            hpPercent.value = playerHp.curHp / playerHp.maxHp;
            expPercent.value = picker.CurExp / picker.curLevelTotalNeedExp;

            level.text = "Lv." + picker.level;

            var timerLeftShow = (int)Mathf.Max(0, GamingState.TimeLeft);
            timer.text = timerLeftShow.ToString();
            waveNo.text = GamingState.CurWaveNo.ToString();
            coinsCount.text = picker.coinsCount.ToString();
            hpSb.Clear();
            hpSb.Append((int)playerHp.curHp);
            hpSb.Append("/");
            hpSb.Append((int)playerHp.maxHp);
            hp.text = hpSb.ToString();
            expSb.Clear();
            expSb.Append((int)picker.CurExp);
            expSb.Append("/");
            expSb.Append((int)picker.curLevelTotalNeedExp);
            levelProgress.text = expSb.ToString();
        }
    }
}
