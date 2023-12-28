using CommonBase;
using TMPro;
using UnityEngine;

namespace OfficeWar
{
    public class DescriptionFloatWindow : BaseUI
    {
        public TMP_Text commodityNameText;
        public TMP_Text abstractDescriptionText;
        public TMP_Text detailsDescriptionText;

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

        private void Update()
        {
            //TODO:添加调整，使该信息框始终完全在游戏窗口内。
            //AdjustUIPosition();
        }

        void AdjustUIPosition()
        {
            var rectTransform = this.transform as RectTransform;
            // 获取屏幕的宽高
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // 将UI的四个角转换为屏幕坐标
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            Vector2 minScreenPos = new Vector2(float.MaxValue, float.MaxValue);
            Vector2 maxScreenPos = new Vector2(float.MinValue, float.MinValue);

            if (minScreenPos.x < 0) this.transform.position += new Vector3(-minScreenPos.x, 0, 0);
            if (minScreenPos.y < 0) this.transform.position += new Vector3(-minScreenPos.y, 0, 0);

            for (int i = 0; i < corners.Length; i++)
            {
                Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, corners[i]);

                // 更新最小和最大屏幕坐标
                minScreenPos = Vector2.Min(minScreenPos, screenPos);
                maxScreenPos = Vector2.Max(maxScreenPos, screenPos);
            }

            // 计算UI的宽高
            float uiWidth = maxScreenPos.x - minScreenPos.x;
            float uiHeight = maxScreenPos.y - minScreenPos.y;

            // 计算UI应该在屏幕中的位置
            float targetX = Mathf.Clamp(rectTransform.position.x, uiWidth / 2, screenWidth - uiWidth / 2);
            float targetY = Mathf.Clamp(rectTransform.position.y, uiHeight / 2, screenHeight - uiHeight / 2);

            // 更新UI的位置
            rectTransform.position = new Vector3(targetX, targetY, rectTransform.position.z);
        }
    }
}
