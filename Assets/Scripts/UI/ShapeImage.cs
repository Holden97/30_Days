using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

/// <summary>
/// Imageƫ��
/// </summary>
public class ShapeImage : Image
{
    [SerializeField]
    private Vector2 rtOffset;//����
    [SerializeField]
    private Vector2 rbOffset;//����
    [SerializeField]
    private Vector2 ltOffset;//����
    [SerializeField]
    private Vector2 lbOffset;//����

    /// <summary>
    /// ��ȡƫ����
    /// </summary>
    private Vector3 GetOffset(int i)
    {
        if (i == 0)
        {
            return lbOffset;
        }
        else if (i == 1)
        {
            return ltOffset;
        }
        else if (i == 2)
        {
            return rtOffset;
        }
        else if (i == 3)
        {
            return rbOffset;
        }
        return Vector3.zero;
    }


    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);
        int vertexCount = toFill.currentVertCount;
        for (int i = 0; i < vertexCount; i++)
        {
            UIVertex vertex = new UIVertex();
            toFill.PopulateUIVertex(ref vertex, i);
            vertex.position += GetOffset(i);
            toFill.SetUIVertex(vertex, i);
        }

    }
}
