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
        public CommodityData[] commodityData;

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

        public CommodityData Get(int index)
        {
            return commodityData[index];
        }
        private static ShopManager instance;

        public ShopManager(int slotCount)
        {
            SlotCount = slotCount;
            commodityData = new CommodityData[slotCount];
        }

        public void Sell(int index)
        {
            this.commodityData[index] = null;
            return;
        }

        public void Refresh()
        {
            Instance.commodityData = GameManager.Instance.CreateCommodityData(SlotCount);
            if (UIManager.Instance.Get<ShopPanel>())
            {
                UIManager.Instance.UpdatePanel<ShopPanel>(GetShopData());
            }
        }

        public static Tuple<PlayerPicker, CommodityData[]> GetShopData()
        {
            var picker = GameManager.Instance.player;
            Tuple<PlayerPicker, CommodityData[]> d = new Tuple<PlayerPicker, CommodityData[]>(picker, Instance.commodityData);
            return d;
        }
    }
}
