using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class Health : MonoBehaviour
    {
        public float maxHp;
        public float curHp;

        private void Awake()
        {
            curHp = maxHp;
        }

        public void BeHurt(float damage)
        {
            curHp = Mathf.Max(0, curHp - damage);
        }
    }
}
