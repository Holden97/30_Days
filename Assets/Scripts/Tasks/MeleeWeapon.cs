using UnityEngine;

namespace OfficeWar
{
    public class MeleeWeapon : MonoBehaviour, IWeapon
    {
        public Health owner;
        public float damage = 20;

        public float attackRange = 3;
        public float attackCostTime = 0.2f;

        public float AttackRange { get => attackRange; set => attackRange = value; }
        public float AttackCostTime { get => attackCostTime; set => attackCostTime = value; }

        public Vector3 GetLocalPos()
        {
            return this.transform.localPosition;
        }

        public Transform GetTransform()
        {
            return this.transform;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var curHealth = collision.collider.transform.GetComponent<Health>();
            if (curHealth != null && curHealth != owner)
            {
                curHealth.BeHurt(damage, this.transform);
            }
        }
    }
}
