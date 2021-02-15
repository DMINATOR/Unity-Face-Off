using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneGenerator))]
public class SceneGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SceneGenerator generator = (SceneGenerator)target;
        if (GUILayout.Button("Generate Scene"))
        {
            generator.GenerateScene();
        }
    }
}
