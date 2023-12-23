using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace OfficeWar
{
    public class AttackingTask : Action
    {
        // The transform that the object is moving towards
        public SharedVector3 target;
        public SharedTransform self;
        public float fieldOfAttack = 60;
        public float attackRange = 1;
        public float attackWidth = .5f;
        private ISpeedModifier speedAnimatorModifier;
        public float punchDamge = 50;
        public RaycastHit2D[] result;


        public override void OnAwake()
        {
            base.OnAwake();
            speedAnimatorModifier = self.Value.GetComponent<ISpeedModifier>();
            result = new RaycastHit2D[20];
        }

        public override TaskStatus OnUpdate()
        {
            Attack(target.Value);
            return TaskStatus.Success;
        }

        private void Attack(Vector3 target)
        {
            var curWeapons = self.Value.GetComponentsInChildren<BaseWeapon>();
            if (curWeapons != null)
            {
                foreach (var w in curWeapons)
                {
                    w.Attack(target);
                }
            }
            else
            {
                var dir = speedAnimatorModifier.XSign >= 0 ? Vector3.right : Vector3.left;
                Physics2D.CircleCastNonAlloc(this.transform.position, attackWidth, dir, result, attackRange);

                foreach (var r in result)
                {
                    if (r.collider != null)
                    {
                        if (r.collider.isTrigger || r.collider == self.Value.GetComponent<Collider2D>()
                            || r.collider.transform.GetComponent<Health>() == null)
                            continue;
                        var health = r.collider.transform.GetComponent<Health>();
                        var vectorToCollider = r.collider.transform.position - this.transform.position;
                        if (Vector3.Dot(vectorToCollider, dir) > 0)
                        {
                            health.BeHurt(punchDamge, transform);
                        }
                    }
                }
            }
        }
    }
}

