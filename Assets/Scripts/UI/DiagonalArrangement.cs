using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DG.DemiEditor.DeEditorUtils;

/// <summary>
/// �����������У�б��
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

    }

    private void Update()
    {
        for (int i = 1; i < 5; i++)
        {
            newWeaponDataItem[i].transform.localPosition = newWeaponDataItem[i - 1].transform.localPosition + new Vector3(250, 49, 0);
        }
        if(5 <= newWeaponDataItem.Count) //���������в������е����������
        {
            newWeaponDataItem[5].transform.localPosition = newWeaponDataItem[0].transform.localPosition + new Vector3(0, -300, 0);

            for (int i = 6; i < newWeaponDataItem.Count; i++)
            {
                newWeaponDataItem[i].transform.localPosition = newWeaponDataItem[i - 1].transform.localPosition + new Vector3(250, 49, 0);
            }
        }
    }

}
