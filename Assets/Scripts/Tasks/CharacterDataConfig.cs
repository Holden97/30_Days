using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [CreateAssetMenu(fileName = "new CharacterDataConfig", menuName = "SO/CharacterDataConfig")]
    [Serializable]
    public class CharacterDataConfig : ScriptableObject
    {
        public List<CharacterData> data;

    }
}
