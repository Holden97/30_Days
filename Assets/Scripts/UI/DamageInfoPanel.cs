using CommonBase;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar
{
    public class DamageInfoPanel : BaseUI
    {
        public GameObject DamagePref;

        private void OnEnable()
        {
            ObjectPoolManager.Instance.CreatePool("伤害文字", DamagePref, 10);
            this.EventRegister<HurtEvent>("HURT", OnBeAttacked);
        }

        private void OnBeAttacked(HurtEvent arg0)
        {
            ShowDamageText(arg0.damage, arg0.beAttacked.transform.position);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ObjectPoolManager.Instance.DisposePool("伤害文字");
        }

        public void ShowDamageText(float damage, Vector3 attackedWorldPos)
        {
            var go = ObjectPoolManager.Instance.GetNextObject("伤害文字");
            //var screenPos = Camera.main.WorldToScreenPoint(go.transform.position);
            //go.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, attackedWorldPos);
            go.GetComponentInChildren<TMP_Text>().alpha = 1;
            go.transform.position = attackedWorldPos + (Vector3)Random.insideUnitCircle * 0.5f;
            go.GetComponentInChildren<DamageText>().Init(damage);
            go.transform.DOBlendableMoveBy(Vector3.up * 2, 0.5f).OnComplete(() =>
            {
                DOTween.To(() => go.GetComponentInChildren<TMP_Text>().alpha, x => go.GetComponentInChildren<TMP_Text>().alpha = x, 0, 1).OnComplete(() =>
                {
                    ObjectPoolManager.Instance.Putback("伤害文字", go);

                });
            });
        }
    }
}
