using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class GameManager : MonoBehaviour
    {
        public GameObject MonsterPrefab;
        private void Start()
        {
            new Timer(2, "生成怪物", onComplete: () =>
            {
                GameObject.Instantiate(MonsterPrefab, new Vector3(Random.Range(-10, 10f), Random.Range(-10, 10f), -1), Quaternion.identity);
            }, isLoop: true).Register();
            UIManager.Instance.ShowPanel<HpPanel>();
        }

        private void Update()
        {
            TimerManager.Instance.Tick();
        }
    }
}
