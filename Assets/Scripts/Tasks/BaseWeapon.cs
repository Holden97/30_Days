using CommonBase;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OfficeWar
{
    public class BaseWeapon : MonoBehaviour
    {
        public int HitCount { get; protected set; }
        public List<Health> HealthsAttacking { get; protected set; }

        public Animator weaponAnimator;
        public Health Owner { get; private set; }
        public bool IsAttacking { get; private set; }
        public float damage = 20;

        public float attackRange = 3;
        public float attackCostTime = 0.2f;

        public float AttackCostTime { get => attackCostTime; set => attackCostTime = value; }

        public float AttackRange { get => attackRange; set => attackRange = value; }

        public virtual void Attack()
        {
            weaponAnimator.SetTrigger("Attack");
        }

        private void Awake()
        {
            Owner = GetComponentInParent<Health>();
            HealthsAttacking = new List<Health>();
        }

        public void ResetAttack()
        {
            HealthsAttacking.Clear();
            IsAttacking = false;
        }

        public void Attacking()
        {
            IsAttacking = true;
        }

        protected virtual void Update()
        {
            if (IsAttacking)
            {
                return;
            }
            Vector2 direction = (InputUtils.GetMouseWorldPosition() - this.transform.position).normalized;
            transform.up = direction;
        }
    }
}