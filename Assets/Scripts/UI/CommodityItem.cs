using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class CommodityItem : MonoBehaviour, IListItem
    {
        public Image avatarImg;
        public Text commodityNameText;
        public Text commodityCost;

        public void BindData(object data)
        {
            var c = data as CommodityData;
            avatarImg.sprite = c.avatar;
            commodityNameText.text = c.name;
            commodityNameText.text = c.cost.ToString();
        }
    }
}

