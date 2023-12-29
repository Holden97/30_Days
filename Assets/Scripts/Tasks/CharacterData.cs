using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [System.Serializable]
    public class CharacterData
    {
        public string name;
        public Sprite avatar;
        public float baseMapHp;
        public float baseSpeed;
        public int shieldCountBeforePerWave;
        public float damageIncreasementPersent;
        public float attackSpeed;
        public GameObject characterPrefab;
    }
}
