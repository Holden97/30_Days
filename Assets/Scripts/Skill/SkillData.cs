using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    [CreateAssetMenu(fileName = "new SkillData", menuName = "SO/Skill Data")]
    public class SkillData : ScriptableObject
    {
        public string skillName;
        public float skillDuration;
        public BehaviorTree skillBT;
    }
}

