using BehaviorDesigner.Runtime;
using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class Health : MonoBehaviour
    {
        public float maxHp;
        [HideInInspector]
        public float curHp;
        public Animator selfAnim;
        public Rigidbody2D selfRigid;
        public float punchRate = 2;

        public bool IsAlive => curHp > 0;

        public void ResetHealth()
        {
            curHp = maxHp;
        }

        private void Awake()
        {
            ResetHealth();
        }

        public void BeHurt(float damage, Transform caster)
        {
            curHp = Mathf.Max(0, curHp - damage);
            selfRigid.AddForce(((Vector2)(this.transform.position - caster.position)).normalized * punchRate, ForceMode2D.Impulse);
            if (curHp <= 0)
            {
                selfAnim.SetBool("Alive", false);
                if (this.tag == "Trainee")
                {
                    StartCoroutine(Putback());
                }
            }
        }

        private IEnumerator Putback()
        {
            yield return new WaitForSeconds(1.5f);
            ObjectPoolManager.Instance.Putback("怪物", gameObject);
            gameObject.GetComponent<BehaviorTree>().DisableBehavior();
        }
    }
}
