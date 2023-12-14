using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace OfficeWar
{
    public class AttackingTask : Action
    {
        // The transform that the object is moving towards
        public SharedTransform target;
        public float fieldOfAttack = 60;
        public float attackRange = 1;
        private Health targetHealth;
        private SpeedAnimatorModifier speedAnimatorModifier;
        public float punchDamge = 10;


        public override void OnAwake()
        {
            base.OnAwake();
            speedAnimatorModifier = this.transform.GetComponent<SpeedAnimatorModifier>();
        }

        public override TaskStatus OnUpdate()
        {
            Attck(target);
            return TaskStatus.Success;
        }

        public void Attck(SharedTransform target)
        {
            Vector3 direction = target.Value.position - transform.position;

            if (target != null)
            {
                targetHealth = target.Value.GetComponent<Health>();
                if (Vector3.Angle(direction, speedAnimatorModifier.lastXGreaterThan0 >= 0 ? Vector3.right : Vector3.left) < fieldOfAttack && Vector3.Distance(target.Value.position, transform.position) < attackRange)
                {
                    targetHealth.BeHurt(punchDamge);
                }
            }
        }
    }
}

