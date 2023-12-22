using CommonBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class GamingInfoPanel : BaseUI
    {
        public Slider hpPercent;
        public TMP_Text hp;
        private GameObject player;
        private PlayerPicker picker;
        private Health playerHp;
        public TMP_Text timer;
        public TMP_Text waveNo;
        public TMP_Text coinsCount;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHp = player.GetComponent<Health>();
            picker = playerHp.GetComponent<PlayerPicker>();
        }

        private void Update()
        {
            hpPercent.value = playerHp.curHp / playerHp.maxHp;

            var timerLeftShow = (int)Mathf.Max(0, GamingState.TimeLeft);
            timer.text = timerLeftShow.ToString();
            waveNo.text = GamingState.CurWaveNo.ToString();
            coinsCount.text = picker.coinsCount.ToString();
            hp.text = $"{playerHp.curHp}/{playerHp.maxHp}";
        }
    }
}
