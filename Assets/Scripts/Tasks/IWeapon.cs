using UnityEngine;

namespace OfficeWar
{
    internal interface IWeapon
    {
        Transform GetTransform();
        Vector3 GetLocalPos();
        float AttackCostTime { get; set; }

        public float AttackRange { get; set; }
    }
}