using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Cinemachine.Utility;
using System.Linq;
using UnityEngine;

namespace OfficeWar
{
    public class EnemyMeleeAttackingTask : Action
    {
        // The transform that the object is moving towards
        public SharedTransform target;
        public SharedTransform self;
        public float fieldOfAttack = 60;
        public float attackRange = 2;
        private ISpeedModifier speedAnimatorModifier;
        public float punchDamge = 50;
        private Collider2D[] result;


        public override void OnAwake()
        {
            base.OnAwake();
            speedAnimatorModifier = self.Value.GetComponent<ISpeedModifier>();
            result = new Collider2D[20];
        }

        public override TaskStatus OnUpdate()
        {
            Attack(target.Value.position);
            return TaskStatus.Success;
        }

        private void Attack(Vector3 target)
        {
            var dir = target - self.Value.position;
            var selfColliders = self.Value.GetComponentsInParent<Collider2D>().ToList();
            Physics2D.OverlapCircleNonAlloc(this.transform.position, attackRange, result);

            foreach (var r in result)
            {
                if (r == null || (selfColliders != null && selfColliders.Contains(r)) || r.GetComponentInChildren<Health>() == null)
                    continue;
                var health = r.GetComponentInChildren<Health>();
                var vectorToCollider = r.transform.position - this.transform.position;
                if (Vector3.Dot(vectorToCollider, dir) > 0)
                {
                    health.BeHurt(punchDamge, transform, this.transform.position);
                }
            }
        }
    }
}

