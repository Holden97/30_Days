using CommonBase;
using UnityEngine;

namespace OfficeWar
{
    public class RangeWeapon : BaseWeapon
    {
        public float attckRange;
        //public GameObject bulletPref;
        private Vector2 fireDir;


        public override void Attack(Vector3 target)
        {
            base.Attack(target);
            //weaponAnimator.SetTrigger("Attack");
            var go = ObjectPoolManager.Instance.GetNextObject("子弹");
            go.GetComponent<Bullet>().Init(Owner);
            go.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
        }
    }
}
