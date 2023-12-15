using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace OfficeWar
{
    public class AttackPreTask : Action
    {
        private Animator selfAnimator;
        public SharedTransform self;

        public override void OnAwake()
        {
            base.OnAwake();
            selfAnimator = self.Value.GetComponent<Animator>();
        }

        public override void OnStart()
        {
            base.OnStart();
            selfAnimator.SetTrigger("Attack");
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}

