using BehaviorDesigner.Runtime;
using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private FiniteStateMachine gameFsm;
        public FSMSO gameFsmSO;

        public GameObject MonsterPrefab;
        public GameObject BulletPrefab;

        protected override void Awake()
        {
            base.Awake();
            gameFsm = new FiniteStateMachine(gameFsmSO);
            gameFsm.Start();
        }

        private void Update()
        {
            gameFsm.Update();
        }
    }
}
