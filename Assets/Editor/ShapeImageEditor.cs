using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

/// <summary>
/// ImageÆ«ÒÆOffset
/// </summary>
[CustomEditor(typeof(ShapeImage), true)]
public class ShapeImageEditor : ImageEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ShapeImage image = (ShapeImage)target;

        SerializedProperty sp = serializedObject.FindProperty("rtOffset");
        EditorGUILayout.PropertyField(sp, new GUIContent("ÓÒÉÏÆ«ÒÆ"));
        SerializedProperty sp1 = serializedObject.FindProperty("rbOffset");
        EditorGUILayout.PropertyField(sp1, new GUIContent("ÓÒÏÂÆ«ÒÆ"));
        SerializedProperty sp2 = serializedObject.FindProperty("ltOffset");
        EditorGUILayout.PropertyField(sp2, new GUIContent("×óÉÏÆ«ÒÆ"));
        SerializedProperty sp3 = serializedObject.FindProperty("lbOffset");
        EditorGUILayout.PropertyField(sp3, new GUIContent("×óÏÂÆ«ÒÆ"));

        serializedObject.ApplyModifiedProperties();
    }
}
