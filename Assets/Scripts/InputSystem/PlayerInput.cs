using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class PlayerInput : MonoBehaviour
    {
        public GameObject player;
        public Animator playerAnim;
        public SpriteRenderer playerRenderer;

        private Vector3 playerSpeed = default;
        private float lastXGreaterThan0 = 0;
        public Vector3 realSpeed = default;

        [Range(2, 6)] public float speedMagnitude = 3;

        void Update()
        {
            playerSpeed = GetSpeed();

            player.transform.position += playerSpeed;
            playerRenderer.flipX = lastXGreaterThan0 < 0;
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
                realSpeed = result * speedMagnitude;
                result = realSpeed * Time.deltaTime;

                if (x != 0)
                {
                    lastXGreaterThan0 = x;
                }
            }

            playerAnim.SetFloat("Speed", realSpeed.magnitude);

            return result;
        }
    }
}
