using CommonBase;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class SweapHitWeapon : BaseWeapon
    {
        public Animator weaponAnimator;
        public float attckRotationAngle;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (IsAttacking)
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
            weaponAnimator.SetTrigger("Attack");
            var originalRotation = transform.rotation;
            var q1 = Quaternion.AngleAxis(attckRotationAngle, Vector3.back);
            var mousePos = InputUtils.GetMouseWorldPosition();
            var orginalPos = transform.localPosition;
            transform.DOLocalRotateQuaternion(originalRotation * q1, AttackSpeed / 2).OnComplete(() =>
            {
                transform.DOLocalRotateQuaternion(originalRotation, AttackSpeed / 2);
            });
        }
    }
}
