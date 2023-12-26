using UnityEngine;

namespace OfficeWar
{
    public interface ISpeedModifier
    {
        void SetSpeed(Vector3 speed);

        Vector3 GetSpeed();

        float XSign { get; set; }
    }
}