using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class HurtEvent
    {
        public Health beAttacked;
        public float damage;
        public Vector3 damageSource;

        public HurtEvent(Health beAttacked, float damage, Vector3 damageSource)
        {
            this.beAttacked = beAttacked;
            this.damage = damage;
            this.damageSource = damageSource;
        }
    }
}
