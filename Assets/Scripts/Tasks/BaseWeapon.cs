using BehaviorDesigner.Runtime;
using CommonBase;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class BaseWeapon : MonoBehaviour, ICost
    {
        public WeaponData weaponData;

        public float attackCD => weaponData.attackSpeed;
        public int HitCount { get; protected set; }
        public List<Health> HealthsAttacking { get; protected set; }

        public Health Owner { get; private set; }
        /// <summary>
        /// 进展武器碰撞判定
        /// </summary>
        public bool AttackingChecking { get; protected set; }

        public BehaviorTree autoAttackBT;

        /// <summary>
        /// 武器是否准备好下次攻击
        /// </summary>
        public bool readyToAttack = true;

        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }

        public int Cost => weaponData.cost;

        public virtual void Attack()
        {
            StartCoroutine(EnterAttackCD());
        }

        protected IEnumerator EnterAttackCD()
        {
            readyToAttack = false;
            yield return new WaitForSeconds(attackCD);
            readyToAttack = true;
        }

        public void AttackingFlag()
        {
            AttackingChecking = true;
        }

        private void Awake()
        {
            Owner = GetComponentInParent<Health>();
            HealthsAttacking = new List<Health>();
            GameObject.Instantiate(autoAttackBT, this.transform);
        }

        public virtual void Init(WeaponData w)
        {
            this.weaponData = w;
            Damage = w.damage;
            AttackSpeed = w.attackSpeed;
            AttackRange = w.attackRange;
        }

        public void ResetAttack()
        {
            HealthsAttacking.Clear();
            AttackingChecking = false;
        }

        protected virtual void Update()
        {
            if (AttackingChecking)
            {
                return;
            }
            Vector2 direction = (InputUtils.GetMouseWorldPosition() - this.transform.position).normalized;
            transform.up = direction;
        }
    }
}