using CommonBase;
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

        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }

        public int Cost => weaponData.cost;

        public virtual void Attack()
        {
        }

        private void Awake()
        {
            Owner = GetComponentInParent<Health>();
            HealthsAttacking = new List<Health>();
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