using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using CommonBase;

namespace OfficeWar
{
    public class Alive : Conditional
    {
        public SharedTransform self;

        private Health selfHealth;

        public override void OnAwake()
        {
            base.OnAwake();
            selfHealth = self.Value.GetComponentInChildren<Health>();
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
