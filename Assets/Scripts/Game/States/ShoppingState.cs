using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using CommonBase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;

namespace OfficeWar
{
    public class ShoppingState : BaseState
    {
        public bool isShopping = false;
        private PlayerPicker picker;
        public ShoppingState(string stateName) : base(stateName)
        {
        }

        public override void OnStateStart()
        {
            base.OnStateStart();
            var player = GameObject.FindGameObjectWithTag("Player");
            ObjectPoolManager.Instance.PutbackAll("怪物");
            picker = player.GetComponent<PlayerPicker>();
            isShopping = true;
            var s = GameManager.Instance.CreateCommodityData();
            Tuple<int, List<CommodityData>> d = new Tuple<int, List<CommodityData>>(picker.coinsCount, s);
            UIManager.Instance.ShowPanel<ShopPanel>(data: d);
        }

        public override void OnStateCheckTransition()
        {
            base.OnStateCheckTransition();
            if (!isShopping)
            {
                this.Transfer("LEAVE_SHOP");
            }
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            UIManager.Instance.Hide<ShopPanel>();
        }
    }
}
