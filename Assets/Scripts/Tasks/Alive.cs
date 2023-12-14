using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace OfficeWar
{
    public class Alive : Conditional
    {
        public SharedTransform target;

        private Health selfHealth;

        public override void OnAwake()
        {
            base.OnAwake();
            selfHealth = target.Value.GetComponent<Health>();
        }

        public override TaskStatus OnUpdate()
        {
            if (selfHealth.IsAlive)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }

    }
}
