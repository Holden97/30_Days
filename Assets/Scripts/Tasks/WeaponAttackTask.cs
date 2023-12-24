using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using CommonBase;
using UnityEngine;

namespace OfficeWar
{
    public class WeaponAttackTask : Action
    {
        public SharedTransform target;
        public SharedWeapon weapon;
        public SharedTransform self;

        public override TaskStatus OnUpdate()
        {
            if (Vector2.Distance(target.Value.position, self.Value.position) < weapon.Value.AttackRange)
                weapon.Value.Attack(target.Value.position);
            return TaskStatus.Success;
        }
    }

    public class SetWeaponRotation : Action
    {
        public SharedTransform target;
        public SharedTransform self;
        public SharedWeapon weapon;

        public override TaskStatus OnUpdate()
        {

            if (target.Value == null)
            {
                weapon.Value.transform.up = Vector3.up;
            }
            else
            {
                Vector2 direction = (target.Value.position - self.Value.position).normalized;
                switch (weapon.Value.weaponData.type)
                {
                    case "近战扫击":
                        weapon.Value.transform.up = direction;
                        break;
                    case "近战直击":
                        weapon.Value.transform.up = direction;
                        break;
                    case "远程":
                        weapon.Value.transform.right = direction;
                        break;
                    default:
                        break;
                }
            }
            return TaskStatus.Success; ;
        }
    }
}

