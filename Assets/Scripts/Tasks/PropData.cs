using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [System.Serializable]
    public class PropData : CommodityData
    {
        public string name;
        public string description;
        public string bytecode;
        public int cost;
        public Sprite avatar;

        public void ExcuteBytecode(string b)
        {

        }
    }
}
