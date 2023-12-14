using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class PlayerInput : MonoBehaviour, ISpeedModifier
    {
        public GameObject player;
        public Animator playerAnim;
        public SpriteRenderer playerRenderer;

        public BehaviorTree punchBt;

        private Vector3 playerSpeed = default;
        private float lastXGreaterThan0 = 0;
        public Vector3 realSpeed = default;
        private Health selfHealth;
        private Rigidbody2D selfRigid;

        [Range(2, 16)] public float speedMagnitude = 10;

        public float LastXGreaterThan0 { get => lastXGreaterThan0; set { lastXGreaterThan0 = value; } }

        private void Awake()
        {
            selfHealth = GetComponent<Health>();
            selfRigid = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (!selfHealth.IsAlive) return;
            realSpeed = GetSpeed();

            selfRigid.MovePosition(player.transform.position + realSpeed * Time.deltaTime);
            playerRenderer.flipX = lastXGreaterThan0 < 0;

            var attacking = Input.GetKey(KeyCode.Mouse0);
            if (attacking)
            {
                punchBt.EnableBehavior();
            }
        }

        private Vector3 GetSpeed()
        {
            var result = Vector3.zero;
            var y = Input.GetAxis("Vertical");
            var x = Input.GetAxis("Horizontal");

            if (y != 0 || x != 0)
            {
                result = new Vector3(x, y);
                if (result.sqrMagnitude > 1)
                {
                    result = result.normalized;
                }
                result *= speedMagnitude;

                if (x != 0)
                {
                    lastXGreaterThan0 = x;
                }
            }

            playerAnim.SetFloat("Speed", result.magnitude);

            return result;
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
