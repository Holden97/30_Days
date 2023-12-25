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
        public ShoppingState(string stateName) : base(stateName)
        {
        }

        public override void OnStateStart()
        {
            Cursor.visible = true;
            base.OnStateStart();
            var player = GameObject.FindGameObjectWithTag("Player");
            ObjectPoolManager.Instance.PutbackAll("怪物");
            isShopping = true;
            ShopManager.Instance.Refresh();
            UIManager.Instance.ShowPanel<ShopPanel>(data: ShopManager.GetShopData());
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
