using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    /// <summary>
    /// 金币与装备管理
    /// </summary>
    public class PlayerPicker : MonoBehaviour
    {
        public int coinsCount;

        public List<BaseWeapon> weapons;
        public List<Prop> props;

        private void Awake()
        {
            coinsCount = 2000;
            weapons = new List<BaseWeapon>();
            props = new List<Prop>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Coin")
            {
                ObjectPoolManager.Instance.Putback("金币", collision.gameObject);
                coinsCount++;
            }
        }

        public ICost Buy(int index)
        {
            CommodityData c = ShopManager.Instance.Get(index);
            if (c == null) { return null; }
            if (this.coinsCount >= c.cost)
            {
                this.coinsCount -= c.cost;
                ShopManager.Instance.Sell(index);
                if (c is WeaponData w)
                {
                    var weapon = WeaponFactory.Instance.CreateWeapon(w);
                    weapons.Add(weapon);
                    return weapon;
                }
                if (c is PropData p)
                {
                    var prop = new Prop(p);
                    props.Add(prop);
                    return prop;
                }
                UIManager.Instance.ShowTip("未找到对应类型！");
                UIManager.Instance.UpdatePanel<ShopPanel>(ShopManager.GetShopData());
                return null;
            }
            else
            {
                UIManager.Instance.ShowTip("金币不足，无法购买！");
                return null;
            }

        }
    }
}
