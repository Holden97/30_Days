using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace OfficeWar
{
    public class MoveToTask : Action
    {
        // The speed of the object
        public float speed = 5;
        // The transform that the object is moving towards
        public SharedTransform target;
        public SharedTransform self;
        private Rigidbody2D selfRigid;
        private Health selfHealth;
        public float inAttackRange;

        public override void OnAwake()
        {
            base.OnAwake();
            selfRigid = self.Value.GetComponent<Rigidbody2D>();
            selfHealth = self.Value.GetComponent<Health>();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            selfRigid.MovePosition(transform.position + speed * (target.Value.position - transform.position).normalized * Time.deltaTime);

        }

        public override TaskStatus OnUpdate()
        {
            // Return a task status of success once we've reached the target
            if (Vector3.Distance(transform.position, target.Value.position) < inAttackRange)
            {
                return TaskStatus.Success;
            }
            if (selfHealth != null && !selfHealth.IsAlive)
            {
                return TaskStatus.Failure;
            }
            // We haven't reached the target yet so keep moving towards it

            var modifier = transform.GetComponent<ISpeedModifier>();
            if (modifier != null)
            {
                modifier.SetSpeed((target.Value.position - transform.position).normalized * speed);
            }
            return TaskStatus.Running;
        }
    }
}
