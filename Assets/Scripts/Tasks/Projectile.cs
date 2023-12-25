using UnityEngine;

namespace OfficeWar
{
    public class Projectile : MonoBehaviour
    {
        public float damage = 15;
        public float speed = 20;
        public Health Owner { get; private set; }
        private void Update()
        {
            this.transform.position += this.transform.right * Time.deltaTime * 20;
        }

        public void Init(Health owner)
        {
            this.Owner = owner;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var h = collision.GetComponentInChildren<Health>();
            if (h && h != Owner&&h.IsAlive)
            {
                h.BeHurt(damage, collision.transform, this.transform.position);
            }
        }
    }
}
