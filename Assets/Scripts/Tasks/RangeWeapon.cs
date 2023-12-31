using CommonBase;
using UnityEngine;

namespace OfficeWar
{
    public class RangeWeapon : BaseWeapon
    {
        public bool needAnimator;
        public Animator animator;
        public float attckRange;
        //public GameObject bulletPref;
        private Vector2 fireDir;
        //public Transform dischargeRefPos;
        public GameObject pistolBullet;


        public override void Attack(Vector3 target)
        {
            if (needAnimator)
            {
                animator.SetTrigger("Attack");
            }
            base.Attack(target);
            //weaponAnimator.SetTrigger("Attack");
        }

        public void CreateRifleBullet()
        {
            var go = ObjectPoolManager.Instance.GetNextObject("子弹");
            go.GetComponent<Projectile>().Init(Owner, WeaponData);
            go.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
        }

        public void CreatePistolBullet()
        {
            var go = GameObject.Instantiate(pistolBullet);
            go.SetActive(true);
            go.GetComponent<Projectile>().Init(Owner, WeaponData);
            go.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
        }
    }
}
