using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class WeaponFactory
    {
        public static BaseWeapon CreateWeapon(WeaponData w)
        {
            GameObject go = GameObject.Instantiate(w.weaponPrefab);
            var curWeapon = go.GetComponent<BaseWeapon>();
            curWeapon.Init(w);
            return curWeapon;
        }
    }
}
