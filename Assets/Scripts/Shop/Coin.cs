using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;

namespace OfficeWar
{
    public class Coin : MonoBehaviour
    {
        private float startTime;
        public AnimationCurve explosionCurve;
        public float explosionDuration;
        public float speedXZ;
        public Vector3 dirXZ;
        private Vector3 initPos;
        /// <summary>
        /// 抛出最大高度
        /// </summary>
        public float explosionHeight;

        public void Init(Vector3 dirXZ, Vector3 pos)
        {
            startTime = Time.time;
            this.transform.position = pos;
            initPos = pos;
            this.dirXZ = dirXZ;
        }

        void Update()
        {
            float timeRatio = Mathf.Clamp01((Time.time - startTime) / explosionDuration);
            if (timeRatio >= 1)
            {
                return;
            }
            float curveValue = explosionCurve.Evaluate(timeRatio);
            transform.position = new Vector3(transform.position.x, initPos.y + curveValue * explosionHeight, transform.position.z) + this.dirXZ * Time.deltaTime;
        }
    }
}
