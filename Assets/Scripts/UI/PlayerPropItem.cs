﻿using CommonBase;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OfficeWar
{
    public class PlayerPropItem : MonoBehaviour, IListItem, IPointerEnterHandler, IPointerExitHandler
    {
        public Image weaponIcon;
        public TMP_Text weaponName;
        public TMP_Text count;

        private PropData propData;
        public void BindData(object data)
        {
            var c = data as Tuple<PropData, int>;
            this.propData = c.Item1;
            if (c != null)
            {
                weaponIcon.sprite = c.Item1.avatar;
                weaponName.text = c.Item1.name; ;
                if (c.Item2 <= 1)
                {
                    count.text = "";
                }
                else
                {
                    count.text = "x" + c.Item2.ToString();
                }
            }
            else
            {
                weaponIcon.sprite = null;
                weaponName.text = "";
                count.text = "";
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.ShowFloatWindow<DescriptionFloatWindow>(this.transform.position, data: propData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.Hide<DescriptionFloatWindow>();
        }
    }
}
