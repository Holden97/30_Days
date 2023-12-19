using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [CreateAssetMenu(fileName = "New PropDataSO", menuName = "SO/PropData")]
    public class PropDataSO : ScriptableObject
    {
        public List<PropData> propData;
    }
}
