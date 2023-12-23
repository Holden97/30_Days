using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OfficeWar
{
    public class DamageText : MonoBehaviour
    {
        public TMP_Text damageText;
        public void Init(float damage)
        {
            damageText.text = ((int)damage).ToString();
        }
    }
}
