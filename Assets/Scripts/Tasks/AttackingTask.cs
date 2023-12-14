using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        public RaycastHit2D[] result;


        public override void OnAwake()
        {
            base.OnAwake();
            speedAnimatorModifier = this.transform.GetComponent<SpeedAnimatorModifier>();
            result = new RaycastHit2D[1];
        }

        public override TaskStatus OnUpdate()
        {
            Attck();
            return TaskStatus.Success;
        }

        public void Attck()
        {
            var dir = speedAnimatorModifier.lastXGreaterThan0 >= 0 ? Vector3.right : Vector3.left;
            Physics2D.CircleCastNonAlloc(this.transform.position, attackRange, dir, result);

            if (result[0].collider != null)
            {
                var health = result[0].collider.transform.GetComponent<Health>();
                var vectorToCollider = result[0].collider.transform.position - this.transform.position;
                if (Vector3.Dot(vectorToCollider, dir) > 0)
                {
                    health.BeHurt(punchDamge);
                }
            }


        }
    }
}

