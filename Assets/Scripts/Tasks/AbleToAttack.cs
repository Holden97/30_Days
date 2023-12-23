using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using OfficeWar;

namespace OfficeWar
{
    public class AbleToAttack : Conditional
    {
        public SharedWeapon weapon;

        public override TaskStatus OnUpdate()
        {
            return weapon.Value.readyToAttack ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}