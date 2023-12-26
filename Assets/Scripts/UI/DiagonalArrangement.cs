using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DG.DemiEditor.DeEditorUtils;

/// <summary>
/// 框体自主排列（斜向）
/// </summary>
public class DiagonalArrangement : MonoBehaviour
{
    public List<GameObject> newWeaponDataItem;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            newWeaponDataItem.Add(child.gameObject);
        }

        for(int i = 1; i < newWeaponDataItem.Count; i++)
        {
            newWeaponDataItem[i].transform.position = newWeaponDataItem[i - 1].transform.position + new Vector3(100, 15, 0);
        }
    }

}
