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
        public bool manualAttackMode = false;
        public WeaponData weaponData;

        public float attackCD => weaponData.attackSpeed;
        public int HitCount { get; protected set; }
        public List<Health> HealthsAttacking { get; protected set; }
        private Health owner;

        public Health Owner
        {
            get
            {
                return owner;
            }
            private set
            {
                autoAttackBTInstance.SetVariableValue("self", value.transform);
                owner = value;
            }
        }
        /// <summary>
        /// 进展武器碰撞判定
        /// </summary>
        public bool AttackingChecking { get; protected set; }

        public BehaviorTree autoAttackBTPref;
        private BehaviorTree autoAttackBTInstance;

        /// <summary>
        /// 武器是否准备好下次攻击
        /// </summary>
        public bool readyToAttack = true;

        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }

        public int Cost => weaponData.cost;

        public virtual void Attack(Vector3 target)
        {
            StartCoroutine(EnterAttackCD());
        }

        protected IEnumerator EnterAttackCD()
        {
            readyToAttack = false;
            yield return new WaitForSeconds(attackCD);
            readyToAttack = true;
        }

        public void SetOwner(Health owner)
        {
            this.Owner = owner;
        }

        public void AttackingFlag()
        {
            AttackingChecking = true;
        }

        private void Awake()
        {
            HealthsAttacking = new List<Health>();
            autoAttackBTInstance = GameObject.Instantiate(autoAttackBTPref, this.transform);
            autoAttackBTInstance.SetVariableValue("curWeapon", this);
        }

        public virtual void Init(WeaponData w)
        {
            this.weaponData = w;
            Damage = w.damage;
            AttackSpeed = w.attackSpeed;
            AttackRange = w.attackRange;
            autoAttackBTInstance.SetVariableValue("attackInterval", this.weaponData.attackSpeed);
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
            if (manualAttackMode)
            {
                //手动攻击
                Vector2 direction = (InputUtils.GetMouseWorldPosition() - this.transform.position).normalized;
                transform.up = direction;
            }
        }
    }
}