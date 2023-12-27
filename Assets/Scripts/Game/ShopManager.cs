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

        public static Tuple<PlayerPicker, ShopData[]> GetShopData()
        {
            var picker = GameManager.Instance.player;
            Tuple<PlayerPicker, ShopData[]> d = new Tuple<PlayerPicker, ShopData[]>(picker, Instance.shopData);
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
