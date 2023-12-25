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
        public bool manualAttackMode = false;
        public int coinsCount;

        public List<BaseWeapon> weapons;
        public List<Prop> props;

        public List<Transform> weaponPositions;

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

            if (c is WeaponData && weapons.Count >= 6)
            {
                Debug.LogWarning("装备已满，无法购买!");
                UIManager.Instance.ShowTip("装备已满，无法购买!");
                return null;
            }

            if (this.coinsCount >= c.cost)
            {
                ShopManager.Instance.Sell(index);

                this.coinsCount -= c.cost;
                if (c is WeaponData w)
                {
                    return SetupWeapon(w);
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

        public BaseWeapon SetupWeapon(WeaponData w)
        {
            var weapon = WeaponFactory.CreateWeapon(w);
            weapons.Add(weapon);
            weapon.SetOwner(GetComponent<Health>());

            foreach (var pos in weaponPositions)
            {
                if (pos.transform.childCount == 0)
                {
                    weapon.transform.SetParent(pos);
                    weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                    break;
                }
            }

            return weapon;
        }
    }
}
