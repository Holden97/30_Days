﻿using CommonBase;
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
            if (readyToAttack)
            {
                var curHealth = collision.transform.GetComponentInChildren<Health>();
                if (curHealth != null && curHealth != Owner && !HealthsAttacking.Contains(curHealth))
                {
                    HealthsAttacking.Add(curHealth);
                    curHealth.BeHurt(Damage, this.transform);
                }
            }
        }



        public override void Attack()
        {
            if (readyToAttack)
            {
                base.Attack();
                weaponAnimator.SetTrigger("Attack");
                Debug.Log("攻击！！");
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

        protected override void Update()
        {
            base.Update();
            weaponAnimator.SetBool("IsAttacking", AttackingCheck);
        }
    }
}
