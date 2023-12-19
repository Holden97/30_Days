using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [CreateAssetMenu(fileName = "New WeaponDataSO", menuName = "SO/WeaponData")]
    public class WeaponDataSO : ScriptableObject
    {
        public List<WeaponData> weaponData;
    }
}
