﻿using BehaviorDesigner.Runtime;
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
        public Faction faction;

        private DamageFlash df;

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

        public void BeHurt(float damage, Transform hitPoint)
        {
            df.CallDamageFlash();
            curHp = Mathf.Max(0, curHp - damage);
            selfRigid.AddForce(((Vector2)(this.transform.position - hitPoint.position)).normalized * punchRate, ForceMode2D.Impulse);
            EventCenter.Instance.Trigger("HURT", new HurtEvent(this, damage));
            if (curHp <= 0)
            {
                selfAnim.SetBool("Alive", false);
                if (faction.factionType == Faction.FactionEnum.ENEMY)
                {
                    var go = ObjectPoolManager.Instance.GetNextObject("金币");
                    go.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
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
