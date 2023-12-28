using CommonBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OfficeWar
{
    public class PlayerWeaponItem : MonoBehaviour, IListItem, IPointerEnterHandler, IPointerExitHandler
    {
        public Image weaponIcon;
        public TMP_Text weaponName;

        private BaseWeapon baseWeapon;
        public void BindData(object data)
        {
            this.baseWeapon = data as BaseWeapon;
            var c = data as BaseWeapon;
            if (c != null)
            {
                weaponIcon.sprite = c.WeaponData.avatar;
                weaponName.text = c.WeaponData.name;
            }
            else
            {
                weaponIcon.sprite = null;
                weaponName.text = "";
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.ShowFloatWindow<DescriptionFloatWindow>(this.transform.position, data: this.baseWeapon.WeaponData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.Hide<DescriptionFloatWindow>();
        }
    }
}
