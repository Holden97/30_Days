using CommonBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class PlayerWeaponItem : MonoBehaviour, IListItem
    {
        public Image weaponIcon;
        public TMP_Text weaponName;
        public void BindData(object data)
        {
            var c = data as BaseWeapon;
            if (c != null)
            {
                weaponIcon.sprite = c.weaponData.avatar;
                weaponName.text = c.weaponData.name;
            }
            else
            {
                weaponIcon.sprite = null;
                weaponName.text = "";
            }
        }
    }
}
