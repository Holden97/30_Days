using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class PlayerPicker : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Coin")
            {

            }
        }
    }
}
