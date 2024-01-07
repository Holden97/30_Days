using BehaviorDesigner.Runtime;
using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class Alert : MonoBehaviour
    {
        public void AfterAlert()
        {
            var go = ObjectPoolManager.Instance.GetNextObject("怪物");
            go.GetComponentInChildren<Health>().ResetHealth();
            go.GetComponentInChildren<BehaviorTree>().EnableBehavior();
            go.GetComponent<Character>().Init();
            GameManager.Instance.AddCharacter(go.GetComponent<Character>());
            go.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
        }
    }
}
