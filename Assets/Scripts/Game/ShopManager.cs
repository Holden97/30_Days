using CommonBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OfficeWar
{
    public class ShopManager
    {
        public static int SlotCount = 4;
        public ShopData[] shopData;

        public static ShopManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShopManager(SlotCount);
                }
                return instance;
            }
        }

        public ShopData Get(int index)
        {
            return shopData[index];
        }
        private static ShopManager instance;
        public int refreshCost = 5;

        public ShopManager(int slotCount)
        {
            SlotCount = slotCount;
            shopData = new ShopData[slotCount];
        }

        public void Sell(int index)
        {
            this.shopData[index] = null;
            return;
        }

        public void Refresh()
        {
            Instance.shopData = GameManager.Instance.RefreshCommodityData(Instance.shopData);
        }

        public static Tuple<PlayerPicker, ShopData[], Character> GetShopData()
        {
            var picker = GameManager.Instance.player;
            var character = GameManager.Instance.player.GetComponent<Character>();
            Tuple<PlayerPicker, ShopData[], Character> d
                = new Tuple<PlayerPicker, ShopData[], Character>(picker, Instance.shopData, character);
            return d;
        }
    }

    [Serializable]
    public class ShopData
    {
        public CommodityData commodityData;
        public bool isLocked;

        public ShopData(CommodityData commodityData)
        {
            this.commodityData = commodityData;
            isLocked = false;
        }

    }
}
