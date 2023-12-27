using CommonBase;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class PlayerPropItem : MonoBehaviour, IListItem
    {
        public Image weaponIcon;
        public TMP_Text weaponName;
        public TMP_Text count;
        public void BindData(object data)
        {
            var c = data as Tuple<PropData, int>;
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
    }
}
