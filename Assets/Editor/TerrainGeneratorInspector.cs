using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(TerrainGenerator))]
public class TerrainGeneratorInspector : Editor
{
    bool autoGenerate = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        autoGenerate = EditorGUILayout.Toggle("Auto Generate", autoGenerate);
        if(autoGenerate) {
            TerrainGenerator generator = (TerrainGenerator) target;
            generator.GenerateTerrain();
        }
    }
}
