using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace OfficeWar
{

    public class IdleTask : Action
    {
        public SharedTransform target;

        public override TaskStatus OnUpdate()
        {
            var modifier = transform.GetComponent<SpeedAnimatorModifier>();
            if (modifier)
            {
                modifier.SetSpeed(Vector3.zero);
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}
