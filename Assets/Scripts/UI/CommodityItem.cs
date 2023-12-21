using BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem;
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
        public int index;

        private CommodityData c;

        public void BindData(object data)
        {
            if (data != null)
            {
                avatarImg.gameObject.SetActive(true);
                var c = data as CommodityData;
                avatarImg.sprite = c.avatar;
                commodityNameText.text = c.name;
                commodityCost.text = c.cost.ToString();
                this.c = c;
            }
            else
            {
                avatarImg.gameObject.SetActive(false);
                avatarImg.sprite = null;
                commodityNameText.text = "";
                commodityCost.text = "";
                this.c = null;
            }

        }

        public void Buy()
        {
            GameManager.Instance.player.Buy(index);
            UIManager.Instance.UpdatePanel<ShopPanel>(ShopManager.GetShopData());
        }

    }
}

