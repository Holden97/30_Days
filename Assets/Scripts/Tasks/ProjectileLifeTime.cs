using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class ProjectileLifeTime : MonoBehaviour
    {
        [Range(2, 5)] public float duration;

        private void OnEnable()
        {
            StartCoroutine(lifeTimer());
        }

        private IEnumerator lifeTimer()
        {
            yield return new WaitForSeconds(duration);
            this.gameObject.SetActive(false);
            ObjectPoolManager.Instance.Putback("子弹", this.gameObject);
        }
    }
}
