using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(CableTilePlacer))]
public class CableTilePlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CableTilePlacer cableTile = (CableTilePlacer)target;
        DrawDefaultInspector();

        if(GUILayout.Button("Draw"))
        {
            cableTile.GenerateCable();
        }
    }
}
#endif
