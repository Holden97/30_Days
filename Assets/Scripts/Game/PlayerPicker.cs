using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class PlayerPicker : MonoBehaviour
    {
        public int coinsCount;

        private void Awake()
        {
            coinsCount = 0;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Coin")
            {
                ObjectPoolManager.Instance.Putback("金币", collision.gameObject);
                coinsCount++;
            }
        }
    }
}
