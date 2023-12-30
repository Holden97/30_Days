using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar.Test
{
    public class TestRigidbody2D : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float forceMagnitude;
        public void AddForce()
        {
            rb.AddForce(Vector2.up * forceMagnitude, ForceMode2D.Impulse); ;
        }
    }
}
