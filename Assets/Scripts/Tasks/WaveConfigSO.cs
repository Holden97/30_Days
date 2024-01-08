using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [CreateAssetMenu(fileName = "New WaveConfigSO", menuName = "SO/WaveConfigSO")]
    public class WaveConfigSO : ScriptableObject
    {
        public List<WaveConfig> wavesConfig;
    }
}
