using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class Faction : MonoBehaviour
    {
        public FactionEnum factionType;
        public enum FactionEnum
        {
            PLAYER = 0,
            ENEMY = 1,
            /// <summary>
            /// 中立
            /// </summary>
            NEUTRALITY = 2
        }
    }
}
