using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

/// <summary>
/// Image偏移
/// </summary>
public class ShapeImage : Image
{
    [SerializeField]
    private Vector2 rtOffset;//右上
    [SerializeField]
    private Vector2 rbOffset;//右下
    [SerializeField]
    private Vector2 ltOffset;//左上
    [SerializeField]
    private Vector2 lbOffset;//左下

    /// <summary>
    /// 获取偏移量
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
