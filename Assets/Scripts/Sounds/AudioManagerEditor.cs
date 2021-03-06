using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AudioManager manager = (AudioManager)target;

        if (GUILayout.Button("Refresh"))
        {
            manager.CreateAssistants();
        }
        DrawDefaultInspector();
    }
}
#endif
