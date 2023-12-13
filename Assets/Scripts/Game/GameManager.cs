using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            UIManager.Instance.ShowPanel<HpPanel>();
        }
    }
}
