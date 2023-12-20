using CommonBase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Sprites;
using UnityEngine;

namespace OfficeWar
{
    public class ShopPanel : BaseUI
    {
        public CommonList shopItemList;
        public TMP_Text coinsText;
        private PlayerPicker playerPicker;
        private GameObject player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerPicker = player.GetComponent<PlayerPicker>();
        }

        public override void UpdateView(object o)
        {
            var s = o as Tuple<int, List<CommodityData>>;
            if (s != null)
            {
                this.coinsText.text = s.Item1.ToString();
                this.shopItemList.BindData(s.Item2);
            }
        }

        public void NextLevel()
        {
            var shoppingState = GameManager.Instance.GameFsm.GetState("购物") as ShoppingState;
            shoppingState.isShopping = false;
        }

        public void Refresh()
        {
            var s = GameManager.Instance.CreateCommodityData();
            UpdateView(new Tuple<int, List<CommodityData>>(playerPicker.coinsCount, s));
        }
    }
}
