using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace OfficeWar
{
    public class OutAttackRange : Conditional
    {
        public float attackRange;
        // Set the target variable when a target has been found so the subsequent tasks know which object is the target
        public SharedTransform target;

        public override TaskStatus OnUpdate()
        {
            if (Vector2.Distance(target.Value.position, transform.position) > attackRange)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }

    }
}
