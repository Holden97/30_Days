using CommonBase;
using UnityEngine;

namespace OfficeWar
{
    public class RangeWeapon : BaseWeapon
    {
        public float attckRange;
        //public GameObject bulletPref;
        private Vector2 fireDir;


        public override void Attack()
        {
            base.Attack();
            //weaponAnimator.SetTrigger("Attack");
            var mousePos = InputUtils.GetMouseWorldPosition();
            var orginalPos = transform.localPosition;
            var go = ObjectPoolManager.Instance.GetNextObject("子弹");
            go.GetComponent<Bullet>().Init(Owner);
            go.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
        }

        protected override void Update()
        {
            if (IsAttacking)
            {
                return;
            }
            fireDir = (InputUtils.GetMouseWorldPosition() - this.transform.position).normalized;
            transform.right = fireDir;
        }
    }
}
