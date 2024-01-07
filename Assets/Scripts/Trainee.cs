using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Trainee : MonoBehaviour
{
    public GameObject head;

    /// <summary>
    /// »»Í·
    /// </summary>
    /// <param name="stateNum"></param>
    public void ChangeHead(int stateNum)
    {
        var spriteResolvers = head.GetComponent<SpriteResolver>();
        spriteResolvers.SetCategoryAndLabel("Head", stateNum.ToString());
    }
}
