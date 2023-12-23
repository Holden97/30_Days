using CommonBase;
using DG.Tweening;
using UnityEngine;

namespace OfficeWar
{
    public class DirectlyHitWeapon : BaseWeapon
    {

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (AttackingChecking)
            {
                var curHealth = collision.transform.GetComponent<Health>();
                if (curHealth != null && curHealth != Owner && !HealthsAttacking.Contains(curHealth))
                {
                    HealthsAttacking.Add(curHealth);
                    curHealth.BeHurt(Damage, this.transform);
                }
            }
        }

        public override void Attack()
        {
            base.Attack();
            var mousePos = InputUtils.GetMouseWorldPosition();
            var orginalPos = transform.localPosition;
            AttackingChecking = true;
            transform.DOLocalMove(((mousePos.To2() - transform.position.To2()).normalized.To3() * AttackRange + orginalPos), AttackSpeed / 2).OnComplete(() =>
            {
                transform.DOLocalMove(orginalPos, AttackSpeed / 2).OnComplete(() => this.AttackingChecking = false);
                HealthsAttacking.Clear();
            });
        }
    }
}
