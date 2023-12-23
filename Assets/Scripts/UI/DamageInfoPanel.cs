using CommonBase;
using DG.Tweening;
using UnityEngine;

namespace OfficeWar
{
    public class DamageInfoPanel : BaseUI
    {
        public GameObject DamagePref;

        private void OnEnable()
        {
            ObjectPoolManager.Instance.CreatePool("伤害文字", DamagePref, 100);
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
            var screenPos = Camera.main.WorldToScreenPoint(go.transform.position);
            go.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, attackedWorldPos);
            go.GetComponent<DamageText>().Init(damage);
            go.transform.DOMoveY(100, 1).OnComplete(() =>
            {
                ObjectPoolManager.Instance.Putback("伤害文字", go);
            });
        }
    }
}
