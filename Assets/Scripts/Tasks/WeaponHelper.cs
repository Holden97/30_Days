using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OfficeWar
{
    public class WeaponHelper : MonoBehaviour
    {
        public UnityEvent OnStart;
        public UnityEvent OnEnd;

        public void TriggerStartAttacking()
        {
            OnStart?.Invoke();
        }

        public void TriggerEndAttacking()
        {
            OnEnd?.Invoke();
        }
    }
}
