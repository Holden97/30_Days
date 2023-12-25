using CommonBase;
using UnityEngine;

namespace OfficeWar
{
    public class Projectile : MonoBehaviour
    {
        public float damage = 15;
        public float speed = 20;
        private WeaponData weaponData;
        private int penetration;
        public Health Owner { get; private set; }
        private void Update()
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
                if (this.penetration > 0)
                {
                    h.BeHurt(damage, collision.transform, this.transform.position);
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
