using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class HurtEvent
    {
        public Health beAttacked;
        public float damage;

        public HurtEvent(Health beAttacked, float damage)
        {
            this.beAttacked = beAttacked;
            this.damage = damage;
        }
    }
}
