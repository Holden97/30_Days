using CommonBase;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public FiniteStateMachine GameFsm { get; private set; }
        public FSMSO gameFsmSO;
        public float waveDuration = 30;

        public GameObject MonsterPrefab;
        public GameObject BulletPrefab;
        public GameObject CoinPrefab;

        public List<WeaponData> weaponsData;
        public List<PropData> propsData;
        public List<Character> characters;

        public PropDataSO propDataSO;
        public WeaponDataSO weaponDataSO;

        public PlayerPicker player;
        public GameDataSave gameData;

        protected override void Awake()
        {
            base.Awake();
            gameData = new GameDataSave();
            GameFsm = new FiniteStateMachine(gameFsmSO);
            GameFsm.Start();
            this.characters = new List<Character>();

            //add data from json file
            TextAsset weaponsJson = Resources.Load<TextAsset>("JSON/Weapons");
            TextAsset propsJson = Resources.Load<TextAsset>("JSON/Props");

            if (weaponsJson != null && weaponDataSO == null)
            {
                var w = JsonUtility.FromJson<JsonWrapper<WeaponData>>(weaponsJson.text);
                weaponDataSO.weaponData = new List<WeaponData>(w.items);
            }
            if (propsJson != null && propDataSO == null)
            {
                var w = JsonUtility.FromJson<JsonWrapper<PropData>>(propsJson.text);
                propDataSO.propData = new List<PropData>(w.items);
            }

            this.weaponsData = weaponDataSO.weaponData;
            this.propsData = propDataSO.propData;
        }

        public CommodityData[] CreateCommodityData(int count)
        {
            var result = new CommodityData[count];
            for (int i = 0; i < count; i++)
            {
                if (Random.Range(0, 1f) < 0.5f)
                {
                    result[i] = (weaponsData.Random());
                }
                else
                {
                    result[i] = (propsData.Random());
                }
            }
            return result;
        }

        public WeaponData GetWeaponData(string weaponName)
        {
            return weaponsData.Find(x => x.name == weaponName);
        }

        private void Update()
        {
            GameFsm.Update();
        }

        internal Character GetCharacter(int characterId)
        {
            return null;
        }
    }
}
