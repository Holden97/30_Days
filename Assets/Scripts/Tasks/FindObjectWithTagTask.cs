using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tasks
{
    public class FindObjectWithTagTask : Conditional
    {
        public string tag;
        public SharedTransform target;

        public override TaskStatus OnUpdate()
        {
            var objectCanFind = GameObject.FindGameObjectWithTag(tag);
            if (objectCanFind != null)
            {
                target.SetValue(objectCanFind.transform);
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}