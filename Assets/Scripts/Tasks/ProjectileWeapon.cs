using CommonBase;
using UnityEngine;

namespace OfficeWar
{
    /// <summary>
    /// 投掷类武器
    /// </summary>
    public class ProjectileWeapon : BaseWeapon
    {
        //public GameObject bulletPref;
        public Animator animator;
        public GameObject pizza;


        public override void Attack(Vector3 target)
        {
            base.Attack(target);
            animator.SetTrigger("Attack");
        }

        public void OnEndAttacking()
        {
            GameObject go = GameObject.Instantiate(pizza);
            go.GetComponent<Projectile>().Init(Owner);
            go.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            go.SetActive(true);
        }
    }
}
