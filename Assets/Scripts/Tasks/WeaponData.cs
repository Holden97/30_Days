﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [System.Serializable]
    public class WeaponData : CommodityData
    {
        public string name;
        public float damage;
        public float attackSpeed;
        public float attackRange;
        public string type;
        public int cost;
        public Sprite avatar;
    }
}
