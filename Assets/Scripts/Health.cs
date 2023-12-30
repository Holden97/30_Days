using BehaviorDesigner.Runtime;
using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [RequireComponent(typeof(DamageFlash))]
    public class Health : MonoBehaviour
    {
        public float maxHp;
        [HideInInspector]
        public float curHp;
        public Animator selfAnim;
        public Rigidbody2D selfRigid;
        public float punchRate = 2;
        public float deathRate = 5;
        public Faction faction;
        public GameObject root;
        public float healthRatio = 1;
        private float hitDuration = 0.5f;

        public float RealisticHp
        {
            get
            {
                return curHp * healthRatio;
            }
        }

        private DamageFlash df;
        public bool isUnderAttack;

        public bool IsAlive => curHp > 0;

        public void ResetHealth()
        {
            curHp = maxHp;
        }

        private void Awake()
        {
            ResetHealth();
            df = GetComponent<DamageFlash>();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            isUnderAttack = false;
        }

        public IEnumerator UnderAttack()
        {
            isUnderAttack = true;
            yield return new WaitForSeconds(hitDuration);
            isUnderAttack = false;
        }

        public void BeHurt(float damage, Vector3 damageSource, float repulse, Vector2 repulseDir)
        {
            if (!this.gameObject.activeInHierarchy) return;
            df.CallDamageFlash();
            StartCoroutine(UnderAttack());
            curHp = Mathf.Max(0, curHp - damage);
            if (repulse > 0)
                selfRigid.AddForce(repulseDir.normalized * repulse, ForceMode2D.Impulse);
            EventCenter.Instance.Trigger("HURT", new HurtEvent(this, damage, damageSource));
            if (curHp <= 0)
            {
                //selfRigid.AddForce(((Vector2)(this.transform.position - damageSource)).normalized * deathRate, ForceMode2D.Impulse);
                selfAnim.SetTrigger("isDead");
                selfAnim.SetBool("Alive", false);
                if (faction.factionType == Faction.FactionEnum.ENEMY)
                {
                    var go = ObjectPoolManager.Instance.GetNextObject("金币");
                    go.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
                    StartCoroutine(Putback());
                    go.GetComponent<Coin>().Init(new Vector3(repulseDir.x, repulseDir.y, 0), this.transform.position);
                }
            }
        }

        private IEnumerator Putback()
        {
            yield return new WaitForSeconds(1.5f);
            ObjectPoolManager.Instance.Putback("怪物", root);
            gameObject.GetComponent<BehaviorTree>()?.DisableBehavior();
        }
    }
}
