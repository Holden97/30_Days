using BehaviorDesigner.Runtime;
using CommonBase;
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
        public PlayerPicker playerPicker;
        public Transform rendererRoot;

        public List<BehaviorTree> skills;

        private float xSign = 0;
        public Vector3 realSpeed = default;
        private Health selfHealth;
        private Rigidbody2D selfRigid;

        [Range(1, 16)] public float speedMagnitude = 10;

        public float XSign { get => xSign; set { xSign = value; } }

        public float SpeedMagnitude { get; set; }

        private void Awake()
        {
            selfHealth = GetComponent<Health>();
            selfRigid = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var shoppingState = GameManager.Instance.GameFsm.GetState("购物") as ShoppingState;
                if (shoppingState.isShopping) return;
                if (UIManager.Instance.Get<CharacterInfoPanel>() != null
                    && UIManager.Instance.Get<CharacterInfoPanel>().IsShowing)
                {
                    UIManager.Instance.CloseCurrent();
                    Time.timeScale = 1;
                }
                else
                {
                    var player = GameObject.FindGameObjectWithTag("Player");
                    var playerPicker = player.GetComponent<PlayerPicker>();
                    UIManager.Instance.ShowPanel<CharacterInfoPanel>(data: playerPicker);
                    Time.timeScale = 0;
                }
            }

            //手动攻击
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (playerPicker != null && playerPicker.manualAttackMode)
                {
                    foreach (var w in playerPicker.weapons)
                    {
                        w.Attack(InputUtils.GetMouseWorldPositionFixedZ());
                    }
                }
            }
        }

        void FixedUpdate()
        {
            if (!selfHealth.IsAlive) return;

            realSpeed = SetSpeedByPlayer();

            selfRigid.MovePosition(player.transform.position + realSpeed * Time.deltaTime);
            //playerRenderer.flipX = xSign < 0; //注释旧版旋转-ZXY

            if (Input.GetAxis("Horizontal") != 0)
            {
                rendererRoot.transform.localScale = new Vector3(-xSign, 1, 1); //更新旋转方式-ZXY
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

        public Vector3 GetSpeed()
        {
            return this.realSpeed;
        }
    }
}
