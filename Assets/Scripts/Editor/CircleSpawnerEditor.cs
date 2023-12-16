using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CircleSpawner))]
public class CircleSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CircleSpawner spawner = (CircleSpawner) target;

        DrawDefaultInspector();

        if(GUILayout.Button("Spawn"))
        {
            spawner.Spawn();
        }
    }
}
