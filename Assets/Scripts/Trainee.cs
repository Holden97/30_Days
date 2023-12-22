using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Trainee : MonoBehaviour
{
    public int stateNum;
    public SpriteResolver spriteResolvers;


    private void Update()
    {
        spriteResolvers.SetCategoryAndLabel("Head", stateNum.ToString());
    }
}
