using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [System.Serializable]
    public class WeaponData : CommodityData
    {
        public float damage;
        public float attackInterval;
        public float attackRange;
        public string type;
        /// <summary>
        /// 穿透（远程）
        /// </summary>
        public int penetration;
        public GameObject weaponPrefab;
    }
}
