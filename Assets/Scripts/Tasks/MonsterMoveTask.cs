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
        public float speed = 4;
        // The transform that the object is moving towards
        public SharedTransform target;

        public override TaskStatus OnUpdate()
        {
            // Return a task status of success once we've reached the target
            if (Vector3.SqrMagnitude(transform.position - target.Value.position) < 0.1f)
            {
                return TaskStatus.Success;
            }
            // We haven't reached the target yet so keep moving towards it
            transform.position = Vector3.MoveTowards(transform.position, target.Value.position, speed * Time.deltaTime);

            var modifier = transform.GetComponent<SpeedAnimatorModifier>();
            if (modifier)
            {
                modifier.SetSpeed((target.Value.position - transform.position) * speed);
            }
            return TaskStatus.Running;
        }
    }
}
