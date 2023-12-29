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
        public float damageIncreasementPersent;
        public int shieldCountBeforePerWave;

        public ISpeedModifier speed;
        public Health health;
        public PlayerPicker playerPicker;

        public string characterName;
        public Sprite avatar;
        public float baseMapHp;
        public float baseSpeed;
        public float attackSpeed;


        private void Awake()
        {
            speed = GetComponentInChildren<ISpeedModifier>();
            var characterData = GameManager.Instance.GetCharacterData(characterName);
            if (characterData != null)
            {
                this.avatar = characterData.avatar;
                this.baseMapHp = characterData.baseMapHp;
                this.baseSpeed = characterData.baseSpeed;
                this.shieldCountBeforePerWave = characterData.shieldCountBeforePerWave;
                this.damageIncreasementPersent = characterData.damageIncreasementPersent;
                this.attackSpeed = characterData.attackSpeed;
            }
            else
            {
                this.avatar = default;
                this.baseMapHp = 50;
                this.baseSpeed = 5;
                this.shieldCountBeforePerWave = 0;
                this.damageIncreasementPersent = 0;
                this.attackSpeed = 50;
            }

        }

        public void Init()
        {
            this.characterId = seed++;
            damageIncreasementPersent = 0;
        }

        public float RealisticHp => health.RealisticHp;

        public float AttackSpeed { get => playerPicker.baseAttackSpeed; set { playerPicker.baseAttackSpeed = value; } }
    }
}
