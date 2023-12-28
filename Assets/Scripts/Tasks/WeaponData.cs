using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OfficeWar
{
    [System.Serializable]
    public class WeaponData : CommodityData
    {
        public float damage;
        public float attackInterval;
        public float attackRange;
        public string type;
        /// <summary>
        /// 穿透（远程）
        /// </summary>
        public int penetration;
        public GameObject weaponPrefab;

        public override string DetailsDescription
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("伤害：" + damage);
                stringBuilder.AppendLine("攻击间隔：" + attackInterval);
                stringBuilder.AppendLine("攻击范围：" + attackRange);
                stringBuilder.AppendLine("武器种类：" + type);
                stringBuilder.AppendLine("穿透：" + penetration);

                return stringBuilder.ToString();
            }
        }
    }
}
