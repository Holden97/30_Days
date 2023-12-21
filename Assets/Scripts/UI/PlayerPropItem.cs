using CommonBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class PlayerPropItem : MonoBehaviour, IListItem
    {
        public Image weaponIcon;
        public TMP_Text weaponName;
        public void BindData(object data)
        {
            var c = data as Prop;
            if (c != null)
            {
                weaponIcon.sprite = c.propData.avatar;
                weaponName.text = c.propData.name; ;
            }
            else
            {
                weaponIcon.sprite = null;
                weaponName.text = "";
            }
        }
    }
}
