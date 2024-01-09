using CommonBase;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public FiniteStateMachine GameFsm { get; private set; }
        public FSMSO gameFsmSO;
        public float generateSpan = 1;

        public CharacterDataConfig characterDataConfig;

        public GameObject MonsterPrefab;
        public GameObject AlertPrefab;
        public GameObject BulletPrefab;
        public GameObject CoinPrefab;

        public List<WeaponData> weaponsData;
        public List<PropData> propsData;
        public List<Character> characters;

        public PropDataSO propDataSO;
        public WeaponDataSO weaponDataSO;

        public PlayerPicker player;
        public GameDataSave gameData;

        public WaveConfigSO waves;
        public int waveCount;


        protected override void Awake()
        {
            base.Awake();
            waveCount = waves.wavesConfig.Count;
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

        public int GetLevel(int totalExp)
        {
            return totalExp / 20;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waveIndex">start from 1</param>
        /// <returns></returns>
        public int GetCurWaveDuration(int waveIndex)
        {
            if (waveIndex - 1 < 0 || waveIndex > waves.wavesConfig.Count)
            {
                Debug.LogError("out of waveConfig's bound!");
                return -1;
            }
            return waves.wavesConfig[waveIndex - 1].duration;
        }

        public CharacterData GetCharacterData(string characterName)
        {
            return characterDataConfig.data.Find(x => x.name == characterName);
        }

        public ShopData[] RefreshCommodityData(ShopData[] shopData)
        {
            for (int i = 0; i < shopData.Length; i++)
            {
                if (shopData[i] == null || !shopData[i].isLocked)
                    shopData[i] = RandomlyCreateCommodityData();
            }
            return shopData;
        }

        public ShopData RandomlyCreateCommodityData()
        {
            if (Random.Range(0, 1f) < 0.5f)
            {
                return new ShopData(weaponsData.Random());
            }
            else
            {
                return new ShopData(propsData.Random());
            }
        }

        public WeaponData GetWeaponData(string weaponName)
        {
            return weaponsData.Find(x => x.name == weaponName);
        }

        private void Update()
        {
            GameFsm.Update();
        }

        public Character GetCharacter(int characterId)
        {
            return characters.Find(x => x.characterId == characterId);
        }

        public void AddCharacter(Character c)
        {
            this.characters.Add(c);
        }

        public void ClearEnemies()
        {
            this.characters.Clear();
            //重新添加玩家
            this.characters.Add(player.GetComponentInParent<Character>());
        }
    }
}
