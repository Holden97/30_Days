using CommonBase;
using UnityEngine;

namespace OfficeWar
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 20;
        private WeaponData weaponData;
        private int penetration;
        public Health Owner { get; private set; }
        private void FixedUpdate()
        {
            this.transform.position += this.transform.right * Time.deltaTime * 20;
        }

        public void Init(Health owner, WeaponData weaponData)
        {
            this.Owner = owner;
            this.weaponData = weaponData;
            this.penetration = weaponData.penetration;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var h = collision.GetComponentInChildren<Health>();
            if (h && h != Owner && h.IsAlive)
            {
                if (this.penetration >= 0)
                {
                    h.BeHurt(weaponData.damage, this.transform.position, this.weaponData.repulse, this.transform.right);
                    penetration--;
                }
                else
                {
                    ObjectPoolManager.Instance.Putback("子弹", this.gameObject);
                }
            }
        }
    }
}
