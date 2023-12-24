using CommonBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class PlayerWeaponDataItem : MonoBehaviour, IListItem
    {
        public GameObject selectedBg;
        public Image weaponIcon;
        public TMP_Text weaponName;

        public void BindData(object data)
        {
            var c = data as WeaponData;
            if (c != null)
            {
                weaponIcon.sprite = c.avatar;
                weaponName.text = c.name;
            }
            else
            {
                weaponIcon.sprite = null;
                weaponName.text = "";
            }
        }

        public void OnSelected()
        {
            selectedBg.SetActive(true);
        }

        public void OnUnselected()
        {
            selectedBg.SetActive(false);
        }
    }
}
