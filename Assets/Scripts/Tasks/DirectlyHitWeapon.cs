using CommonBase;
using DG.Tweening;
using UnityEngine;

namespace OfficeWar
{
    public class DirectlyHitWeapon : BaseWeapon
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var curHealth = collision.collider.transform.GetComponent<Health>();
            if (curHealth != null && curHealth != Owner)
            {
                curHealth.BeHurt(damage, this.transform);
            }
        }

        public override void Attack()
        {
            base.Attack();
            var mousePos = InputUtils.GetMouseWorldPosition();
            var orginalPos = transform.localPosition;
            transform.DOLocalMove(((mousePos - transform.position).normalized * AttackRange + orginalPos).To2(), AttackCostTime / 2).OnComplete(() =>
            {
                transform.DOLocalMove(orginalPos, AttackCostTime / 2);
            });
        }
    }
}
