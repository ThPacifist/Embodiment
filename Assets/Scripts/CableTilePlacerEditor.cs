using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(CableTilePlacer))]
public class CableTilePlacerEditor : Editor
{
    //Stuff
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Draw"))
        {

        }
    }
}
#endif
