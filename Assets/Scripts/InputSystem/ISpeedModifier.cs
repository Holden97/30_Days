using UnityEngine;

namespace OfficeWar
{
    internal interface ISpeedModifier
    {
        void SetSpeed(Vector3 speed);

        float LastXGreaterThan0 { get; set; }
    }
}