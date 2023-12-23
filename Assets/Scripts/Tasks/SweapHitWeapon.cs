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

        public override void Attack(Vector3 target)
        {
            if (readyToAttack)
            {
                base.Attack(target);
                weaponAnimator.SetTrigger("Attack");
            }
        }

        protected override void Update()
        {
            base.Update();
            weaponAnimator.SetBool("IsAttacking", AttackingChecking);
        }
    }
}
