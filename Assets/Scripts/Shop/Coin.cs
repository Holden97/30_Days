using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class Coin : MonoBehaviour
    {
        private float startTime;
        public AnimationCurve explosionCurve;
        public float explosionDuration;
        public float dropSpeedXZ;
        public Vector3 dropDirXZ;
        private Vector3 initPos;
        public AnimationCurve attractSpeed;
        /// <summary>
        /// 抛出最大高度
        /// </summary>
        public float explosionHeight;

        public void Init(Vector3 dirXZ, Vector3 pos)
        {
            startTime = Time.time;
            this.transform.position = pos;
            initPos = pos;
            this.dropDirXZ = dirXZ;

            StartCoroutine(Drop());
        }

        private IEnumerator Drop()
        {
            while (true)
            {
                float timeRatio = Mathf.Clamp01((Time.time - startTime) / explosionDuration);
                if (timeRatio >= 1)
                {
                    yield break;
                }
                float curveValue = explosionCurve.Evaluate(timeRatio);
                transform.position = new Vector3(transform.position.x, initPos.y + curveValue * explosionHeight, transform.position.z) + this.dropDirXZ * Time.deltaTime;
                yield return null;
            }
        }

        public void BeAttracted(Transform player, Action onComplete)
        {
            StartCoroutine(Attracted(player, onComplete));
        }

        public IEnumerator Attracted(Transform player, Action onComplete)
        {
            var timer = 0f;
            while (true)
            {
                timer += Time.deltaTime;
                var curSpeed = attractSpeed.Evaluate(timer);
                var dir = player.position - this.transform.position;

                transform.position += dir * curSpeed * Time.deltaTime;
                if (Vector3.Distance(this.transform.position, player.position) < .2f)
                {
                    onComplete?.Invoke();
                }
                yield return null;
            }
        }
    }
}
