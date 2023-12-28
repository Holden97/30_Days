using CommonBase;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OfficeWar
{
    public class PlayerWeaponDataItem : MonoBehaviour, IListItem, IPointerExitHandler, IPointerEnterHandler
    {
        public GameObject selectedBg;
        public Image weaponIcon;
        public TMP_Text weaponName;

        private WeaponData w;

        public void BindData(object data)
        {
            w = data as WeaponData;
            if (w != null)
            {
                weaponIcon.sprite = w.avatar;
                weaponName.text = w.name;
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
            GameManager.Instance.gameData.initialWeaponData = w;
        }

        public void OnUnselected()
        {
            selectedBg.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.ShowFloatWindow<DescriptionFloatWindow>(this.transform.position, data: this.w);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.Hide<DescriptionFloatWindow>();
        }
    }
}
