using CommonBase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OfficeWar
{
    public class ObjectPoolDisposer : MonoBehaviour
    {
        [Range(0.3f, 2)] public float duration;
        public UnityEvent onDispose;
        public string poolName;

        private void OnEnable()
        {
            StartCoroutine(Dispose());
        }

        private IEnumerator Dispose()
        {
            yield return new WaitForSeconds(duration);
            onDispose?.Invoke();
            ObjectPoolManager.Instance.Putback(poolName, this.gameObject);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}
