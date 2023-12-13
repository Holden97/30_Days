using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OfficeWar
{
    public class AttackPreTask : Action
    {
        private Animator selfAnimator;

        public override void OnAwake()
        {
            base.OnAwake();
            selfAnimator = this.transform.GetComponent<Animator>();
        }

        public override TaskStatus OnUpdate()
        {
            selfAnimator.SetTrigger("Attack");
            return TaskStatus.Success;
        }
    }
}

