﻿using CommonBase;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OfficeWar
{
    public class CommodityItem : MonoBehaviour, IListItem, IPointerEnterHandler, IPointerExitHandler
    {
        public Image avatarImg;
        public Sprite transparentSprite;
        public TMP_Text commodityNameText;
        public TMP_Text commodityCost;
        public Image lockFlagImg;

        public List<Sprite> lockIcons;

        private CanvasGroup cg;
        public int index;

        private ShopData c;

        private void Awake()
        {
            cg = GetComponent<CanvasGroup>();
        }

        public void BindData(object data)
        {
            if (data != null)
            {
                cg.alpha = 1;
                var c = data as ShopData;
                avatarImg.sprite = c.commodityData.avatar;
                commodityNameText.text = c.commodityData.name;
                commodityCost.text = c.commodityData.cost.ToString();
                this.c = c;
                lockFlagImg.gameObject.SetActive(true);
                lockFlagImg.sprite = c.isLocked ? lockIcons[0] : lockIcons[1];
            }
            else
            {
                cg.alpha = 0;
                avatarImg.sprite = transparentSprite;
                commodityNameText.text = "";
                commodityCost.text = "";
                this.c = null;
                lockFlagImg.gameObject.SetActive(false);
            }

        }

        public void Buy()
        {
            GameManager.Instance.player.Buy(index);
            UIManager.Instance.UpdatePanel<ShopPanel>(ShopManager.GetShopData());
        }

        public void Lock()
        {
            c.isLocked = !c.isLocked;
            ShopManager.Instance.Get(index).isLocked = c.isLocked;
            UIManager.Instance.UpdatePanel<ShopPanel>(ShopManager.GetShopData());
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CommodityData d = default;
            if (this.c != null)
            {
                d = this.c.commodityData;
            }
            UIManager.Instance.ShowFloatWindow<DescriptionFloatWindow>(this.transform.position, data: d);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.Hide<DescriptionFloatWindow>();
        }
    }
}

