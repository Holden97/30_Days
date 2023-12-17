using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Cainos.CustomizablePixelCharacter;
using CommonBase;
using DG.Tweening;
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
        public SharedTransform self;
        public float fieldOfAttack = 60;
        public float attackRange = 1;
        public float attackWidth = .5f;
        private ISpeedModifier speedAnimatorModifier;
        public float punchDamge = 50;
        public RaycastHit2D[] result;


        public override void OnAwake()
        {
            base.OnAwake();
            speedAnimatorModifier = self.Value.GetComponent<ISpeedModifier>();
            result = new RaycastHit2D[20];
        }

        public override TaskStatus OnUpdate()
        {
            Attck();
            return TaskStatus.Success;
        }

        public void Attck()
        {
            var curWeapon = self.Value.GetComponentInChildren<BaseWeapon>();
            if (curWeapon != null)
            {
                curWeapon.Attack();
            }
            else
            {
                //speedAnimatorModifier.XSign = Mathf.Sign(target.Value.position.x - this.transform.position.x);
                var dir = speedAnimatorModifier.XSign >= 0 ? Vector3.right : Vector3.left;
                Physics2D.CircleCastNonAlloc(this.transform.position, attackWidth, dir, result, attackRange);

                foreach (var r in result)
                {
                    if (r.collider != null)
                    {
                        if (r.collider.isTrigger || r.collider == self.Value.GetComponent<Collider2D>()
                            || r.collider.transform.GetComponent<Health>() == null)
                            continue;
                        var health = r.collider.transform.GetComponent<Health>();
                        var vectorToCollider = r.collider.transform.position - this.transform.position;
                        if (Vector3.Dot(vectorToCollider, dir) > 0)
                        {
                            health.BeHurt(punchDamge, transform);
                        }
                    }
                }
            }




        }
    }
}

