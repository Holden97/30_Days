using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace OfficeWar
{
    public class AnimatorModifier : MonoBehaviour, ISpeedModifier
    {
        public Animator playerAnim;
        public Transform playerRenderer;
        public Health selfHealth;
        /// <summary>
        /// 1-right,-1-left
        /// </summary>
        private float dir = 1;
        public Vector3 realSpeed = default;
        public float XSign { get => dir; set { dir = value; } }

        public float SpeedMagnitude { get; set; }

        void Update()
        {
            playerAnim.SetFloat("Speed", realSpeed.magnitude);
            playerRenderer.localScale = new Vector3(-XSign, 1, 1); //更新旋转方式-ZXY
        }

        public void SetSpeed(Vector3 speed)
        {
            this.realSpeed = speed;
            if (speed.x != 0)
            {
                dir = Mathf.Sign(speed.x);
            }
        }

        public Vector3 GetSpeed()
        {
            return realSpeed;
        }
    }
}
