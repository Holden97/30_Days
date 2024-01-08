using CommonBase;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OfficeWar
{
    public class ShopPanel : BaseUI
    {
        public CommonList shopItemList;
        public CommonList playerPropsList;
        public CommonList playerWeaponsList;
        public TMP_Text coinsText;
        private PlayerPicker playerPicker;
        private Character character;
        private GameObject player;

        public TMP_Text speed;
        public TMP_Text hp;
        public TMP_Text shield;
        public TMP_Text enhancedDamgePercent;
        public TMP_Text attackSpeed;
        public TMP_Text refreshCost;


        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerPicker = player.GetComponentInChildren<PlayerPicker>();
            character = player.GetComponent<Character>();
        }

        public override void UpdateView(object o)
        {
            var s = o as Tuple<PlayerPicker, ShopData[], Character>;
            if (s != null)
            {
                this.coinsText.text = s.Item1.coinsCount.ToString();
                this.shopItemList.BindData(s.Item2);
                this.playerWeaponsList.BindData(s.Item1.weapons);
                this.playerPropsList.BindData(CountProps(s.Item1.props));

                this.speed.text = ((int)s.Item3.speed.SpeedMagnitude).ToString();
                this.hp.text = ((int)s.Item3.health.maxHp).ToString();
                this.shield.text = s.Item3.shieldCountBeforePerWave.ToString();
                this.enhancedDamgePercent.text = ((int)s.Item3.damageIncreasementPersent).ToString();
                this.attackSpeed.text = ((int)s.Item3.attackSpeed).ToString();
                this.refreshCost.text = ShopManager.Instance.refreshCost.ToString();
            }
        }

        public List<Tuple<PropData, int>> CountProps(List<Prop> props)
        {
            List<Tuple<PropData, int>> result = new List<Tuple<PropData, int>>();
            Dictionary<PropData, int> propsDic = new Dictionary<PropData, int>();
            foreach (Prop prop in props)
            {
                if (!propsDic.TryAdd(prop.propData, 1))
                {
                    propsDic[prop.propData]++;
                }
            }

            foreach (var item in propsDic)
            {
                result.Add(new Tuple<PropData, int>(item.Key, item.Value));
            }

            return result;
        }

        public void NextLevel()
        {
            var shoppingState = GameManager.Instance.GameFsm.GetState("购物") as ShoppingState;
            shoppingState.isShopping = false;
        }

        public void Roll()
        {
            if (playerPicker.coinsCount - ShopManager.Instance.refreshCost >= 0)
            {
                playerPicker.coinsCount -= ShopManager.Instance.refreshCost;
            }
            else
            {
                return;
            }
            ShopManager.Instance.Refresh();
            UpdateView(new Tuple<PlayerPicker, ShopData[], Character>(playerPicker, ShopManager.Instance.shopData, character));
        }
    }
}
