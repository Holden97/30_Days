using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class WeaponFactory : MonoSingleton<WeaponFactory>
    {
        public GameObject rangeWeaponPref;
        public GameObject directlyHitWeaponPref;
        public GameObject sweapHitWeaponPref;


        public BaseWeapon CreateWeapon(WeaponData w)
        {
            GameObject go;
            switch (w.type)
            {
                case "远程":
                    go = GameObject.Instantiate(rangeWeaponPref);
                    break;
                case "近战直击":
                    go = GameObject.Instantiate(directlyHitWeaponPref);
                    break;
                case "近战扫击":
                    go = GameObject.Instantiate(sweapHitWeaponPref);
                    break;
                default:
                    return null;
            }
            var curWeapon = go.GetComponent<BaseWeapon>();
            curWeapon.Init(w);
            return curWeapon;
        }
    }
}
