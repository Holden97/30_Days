using CommonBase;
using System;
using TMPro;
using UnityEngine;

namespace OfficeWar
{
    public class ShopPanel : BaseUI
    {
        public CommonList shopItemList;
        public CommonList playerPropsList;
        public CommonList playerWeaponsList;
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
            var s = o as Tuple<PlayerPicker, CommodityData[]>;
            if (s != null)
            {
                this.coinsText.text = s.Item1.coinsCount.ToString();
                this.shopItemList.BindData(s.Item2);
                this.playerWeaponsList.BindData(s.Item1.weapons);
                this.playerPropsList.BindData(s.Item1.props);
            }
        }

        public void NextLevel()
        {
            var shoppingState = GameManager.Instance.GameFsm.GetState("购物") as ShoppingState;
            shoppingState.isShopping = false;
        }

        public void Refresh()
        {
            ShopManager.Instance.commodityData = GameManager.Instance.CreateCommodityData(ShopManager.SlotCount);
            UpdateView(new Tuple<PlayerPicker, CommodityData[]>(playerPicker, ShopManager.Instance.commodityData));
        }
    }
}
