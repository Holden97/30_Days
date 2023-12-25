using BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem;
using CommonBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class CommodityItem : MonoBehaviour, IListItem
    {
        public Image avatarImg;
        public Sprite transformSquare;
        public TMP_Text commodityNameText;
        public TMP_Text commodityCost;

        private CanvasGroup cg;
        public int index;

        private CommodityData c;

        private void Awake()
        {
            cg = GetComponent<CanvasGroup>();
        }

        public void BindData(object data)
        {
            if (data != null)
            {
                cg.alpha = 1;
                var c = data as CommodityData;
                avatarImg.sprite = c.avatar;
                commodityNameText.text = c.name;
                commodityCost.text = c.cost.ToString();
                this.c = c;
            }
            else
            {
                cg.alpha = 0;
                avatarImg.sprite = transformSquare;
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

