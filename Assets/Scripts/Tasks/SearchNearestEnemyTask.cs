using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using CommonBase;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OfficeWar
{
    public class SearchNeareastEnemy : Action
    {
        public SharedTransform target;
        public SharedTransform self;

        private List<Collider2D> colliders;
        public override void OnAwake()
        {
            base.OnAwake();
            var enemies = Physics2D.OverlapCircleAll(target.Value.position, 10);

            colliders = enemies.ToList();
            colliders.Sort(new PeopleComparer(self.Value.position));
            if (!colliders.IsNullOrEmpty())
            {
                this.target.SetValue(colliders[0]);
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (target.Value != null)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }

        // 自定义比较方法类
        class PeopleComparer : IComparer<Collider2D>
        {
            public Vector3 playerPos;

            public PeopleComparer(Vector3 playerPos)
            {
                this.playerPos = playerPos;
            }

            /// <summary>
            /// 自定义距离排序器
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(Collider2D x, Collider2D y)
            {
                if (Vector3.Distance(x.transform.position, playerPos) < Vector3.Distance(y.transform.position, playerPos))
                {
                    return -1;
                }
                else if (Vector3.Distance(x.transform.position, playerPos) > Vector3.Distance(y.transform.position, playerPos))
                {
                    return 1;
                }
                return 0;
            }
        }
    }
}
