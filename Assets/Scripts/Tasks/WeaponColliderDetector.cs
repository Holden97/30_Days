using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class WeaponColliderDetector : MonoBehaviour
    {
        public BaseWeapon weapon;
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (weapon.AttackingChecking)
            {
                var curHealth = collision.transform.GetComponentInChildren<Health>();
                if (curHealth != null && curHealth != weapon.Owner && !weapon.HealthsAttacking.Contains(curHealth))
                {
                    weapon.HealthsAttacking.Add(curHealth);
                    curHealth.BeHurt(weapon.Damage, this.transform, this.transform.position);
                }
            }
        }
    }
}
