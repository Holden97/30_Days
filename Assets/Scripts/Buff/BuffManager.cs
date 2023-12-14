using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class BuffManager : MonoSingleton<BuffManager>
    {
        public Dictionary<string, List<BuffInstance>> buffDic;
        protected override void Awake()
        {
            base.Awake();

        }

        private void Update()
        {

        }
    }
}