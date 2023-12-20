using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [System.Serializable]
    public class WeaponData : CommodityData
    {
        public float damage;
        public float attackSpeed;
        public float attackRange;
        public string type;
    }
}
