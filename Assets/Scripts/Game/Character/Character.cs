using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class Character : MonoBehaviour
    {
        /// <summary>
        /// 0给主角，怪物从1开始
        /// </summary>
        public static int seed = 1;
        public int characterId;
        /// <summary>
        /// 伤害增加比例
        /// </summary>
        public float damageEnhancedPercent;
        public int shieldPerWaveBeforeStart;

        public ISpeedModifier speed;
        public Health health;
        public PlayerPicker playerPicker;

        public void Init()
        {
            this.characterId = seed++;
            damageEnhancedPercent = 0;
        }

        public float RealisticHp => health.RealisticHp;

        public float AttackSpeed { get => playerPicker.baseAttackSpeed; set { playerPicker.baseAttackSpeed = value; } }
    }
}
