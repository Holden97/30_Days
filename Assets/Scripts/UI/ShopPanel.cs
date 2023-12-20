using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class ShopPanel : BaseUI
    {
        public CommonList shopItemList;

        public override void UpdateView(object o)
        {
            var s = o as List<CommodityData>;
            if (s != null)
            {
                this.shopItemList.BindData(s);
            }
        }
    }
}
