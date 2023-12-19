using CommonBase;
using System.Collections;
using System.Collections.Generic;
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
            base.OnStateStart();
            isShopping = true;
            UIManager.Instance.ShowPanel<ShopPanel>();
        }

        public override void OnStateCheckTransition()
        {
            base.OnStateCheckTransition();
            if (!isShopping)
            {
                this.Transfer("离开商店");
            }
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            isShopping = false;
        }
    }
}
