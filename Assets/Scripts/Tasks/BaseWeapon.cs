using BehaviorDesigner.Runtime;
using CommonBase;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class BaseWeapon : MonoBehaviour, ICost
    {
        public WeaponData weaponData;
        public int HitCount { get; protected set; }
        public List<Health> HealthsAttacking { get; protected set; }

        public Health Owner { get; private set; }
        public bool IsAttacking { get; protected set; }

        public BehaviorTree autoAttackBT;

        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }

        public int Cost => weaponData.cost;

        public virtual void Attack()
        {
        }

        public void AttackingFlag()
        {
            IsAttacking = true;
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
            IsAttacking = false;
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