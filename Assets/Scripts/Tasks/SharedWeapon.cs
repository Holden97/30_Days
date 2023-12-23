using BehaviorDesigner.Runtime;

namespace OfficeWar
{
    [System.Serializable]
    public class SharedWeapon : SharedVariable<BaseWeapon>
    {
        public static implicit operator SharedWeapon(BaseWeapon value) { return new SharedWeapon { Value = value }; }
    }
}