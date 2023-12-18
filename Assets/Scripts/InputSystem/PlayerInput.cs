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

        public bool isAttacking;

        public BehaviorTree punchBt;

        private float xSign = 0;
        public Vector3 realSpeed = default;
        private Health selfHealth;
        private Rigidbody2D selfRigid;

        [Range(2, 16)] public float speedMagnitude = 10;

        public float XSign { get => xSign; set { xSign = value; } }

        private void Awake()
        {
            selfHealth = GetComponent<Health>();
            selfRigid = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (!selfHealth.IsAlive) return;
            if(Input.GetAxis("Horizontal") == 0) return; //水平操控未输入则返回-ZXY

            realSpeed = SetSpeedByPlayer();

            selfRigid.MovePosition(player.transform.position + realSpeed * Time.deltaTime);
            //playerRenderer.flipX = xSign < 0; //注释旧版旋转-ZXY
            player.transform.localScale = new Vector3(-xSign, 1, 1); //更新旋转方式-ZXY

            var attacking = Input.GetKey(KeyCode.Mouse0);
            if (attacking)
            {
                punchBt.EnableBehavior();
            }
        }

        private Vector3 SetSpeedByPlayer()
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
                    XSign = Math.Sign(x);
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
                XSign = Math.Sign(speed.x);
            }
        }

    }
}
