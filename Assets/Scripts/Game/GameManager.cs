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

        public List<WeaponData> weaponsData;
        public List<PropData> propsData;

        protected override void Awake()
        {
            base.Awake();
            gameFsm = new FiniteStateMachine(gameFsmSO);
            gameFsm.Start();
            this.weaponsData = new List<WeaponData>();
            this.propsData = new List<PropData>();

            //add data from json file
            TextAsset weaponsJson = Resources.Load<TextAsset>("JSON/Weapons");
            TextAsset propsJson = Resources.Load<TextAsset>("JSON/Props");

            if (weaponsJson != null)
            {
                var w = JsonUtility.FromJson<JsonWrapper<WeaponData>>(weaponsJson.text);
                weaponsData = new List<WeaponData>(w.items);
            }
            if (propsJson != null)
            {
                var w = JsonUtility.FromJson<JsonWrapper<PropData>>(propsJson.text);
                propsData = new List<PropData>(w.items);
            }
        }

        private void Update()
        {
            gameFsm.Update();
        }
    }
}
