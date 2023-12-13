using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class AttackTask : Action
    {
        // The speed of the object
        public float speed = 0;
        // The transform that the object is moving towards
        public SharedTransform target;

        public override TaskStatus OnUpdate()
        {
            Attck(target);
            return TaskStatus.Success;
        }

        public void Attck(SharedTransform target)
        {

        }
    }
}

