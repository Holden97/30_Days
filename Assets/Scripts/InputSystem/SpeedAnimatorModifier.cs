using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class SpeedAnimatorModifier : MonoBehaviour, ISpeedModifier
    {
        public Animator playerAnim;
        public SpriteRenderer playerRenderer;
        public Health selfHealth;

        public float lastXGreaterThan0 = 0;
        public Vector3 realSpeed = default;

        [Range(2, 6)] public float speedMagnitude = 3;

        public float XSign { get => lastXGreaterThan0; set { lastXGreaterThan0 = value; } }

        void Update()
        {
            playerAnim.SetFloat("Speed", realSpeed.magnitude);
            playerRenderer.flipX = lastXGreaterThan0 < 0;
        }

        public void SetSpeed(Vector3 speed)
        {
            this.realSpeed = speed;
            if (speed.x != 0)
            {
                lastXGreaterThan0 = speed.x;
            }
        }
    }
}
