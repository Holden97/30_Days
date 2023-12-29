using CommonBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class DescriptionFloatWindow : BaseUI
    {
        public TMP_Text commodityNameText;
        public TMP_Text abstractDescriptionText;
        public TMP_Text detailsDescriptionText;
        private RectTransform rectTransform;
        public Image img;

        public override void UpdateView(object o)
        {
            var d = o as CommodityData;
            if (d != null)
            {
                commodityNameText.text = d.name;
                abstractDescriptionText.text = d.AbstractDescription;
                detailsDescriptionText.text = d.DetailsDescription;
            }
            else
            {
                commodityNameText.text = string.Empty;
                abstractDescriptionText.text = string.Empty;
                detailsDescriptionText.text = string.Empty;
            }
        }

        private void Awake()
        {
            rectTransform = img.GetComponent<RectTransform>();
        }

        private void Update()
        {
            ConstrainFullyInGameWindow();
        }

        /// <summary>
        /// 限制UI完全显示在屏幕内
        /// https://huotuyouxi.com/2021/12/26/unity-tips-017/#%E9%99%90%E5%88%B6-UI-%E8%8C%83%E5%9B%B4
        /// </summary>
        private void ConstrainFullyInGameWindow()
        {
            // UI 的真实坐标
            var pos = rectTransform.anchoredPosition;

            // UI 的大小尺寸
            var size = rectTransform.sizeDelta / 2;

            // 计算屏幕的尺寸
            float xDistance = Screen.width / 2;
            float yDistance = Screen.height / 2;

            // 限制 UI 坐标最大最小值
            float x = Mathf.Clamp(pos.x, -xDistance + size.x, xDistance - size.x);
            float y = Mathf.Clamp(pos.y, -yDistance + size.y, yDistance - size.y);

            // 调整 UI 坐标
            rectTransform.anchoredPosition = new Vector2(x, y);
        }
    }
}
