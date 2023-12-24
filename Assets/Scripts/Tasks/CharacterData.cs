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
        public int mapHp;
        public GameObject characterPrefab;
    }
}
