using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(SceneGeneratorLocator))]
[ExecuteInEditMode]
public class SceneGenerator : MonoBehaviour
{
    //Not exposed

    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public SceneGeneratorLocator Locator;


    public void GenerateScene()
    {
        try
        {
            Debug.Log("Generation started");

            foreach(var setting in Locator.Settings)
            {
                GenerateLayer(setting);
            }

            // Mark changes as permanent
            var activeScene = EditorSceneManager.GetActiveScene();
            if( EditorSceneManager.MarkSceneDirty(activeScene))
            {
                Debug.Log($"Scene {activeScene.name} marked as Dirty");
            }
            else
            {
                Debug.Log($"Scene {activeScene.name} failed to be marked as Dirty");
            }

            Debug.Log("Generation finished");
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
            throw new Exception("Generation failed", ex);
        }
    }

    public void GenerateLayer(SceneGenerationSettings settings)
    {
        try
        {
            Debug.Log($"Generating for settings '{settings}'");

            if ( settings.DestinationLayer == null )
            {
                throw new Exception("No destination layer is provided!");
            }

            if (settings.Sprite == null)
            {
                throw new Exception("No source material is provided!");
            }

            if (settings.Sprite == null)
            {
                throw new Exception("No sorting layer is defined");
            }

            // Remove all child objects
            foreach (Transform child in settings.DestinationLayer.transform)
            {
                DestroyImmediate(child.gameObject);
            }

            // Instantiate new GameObject from Prefab
            GameObject instance = Instantiate(Locator.GeneratedTerrainPrefab, Vector3.zero, Quaternion.identity, settings.DestinationLayer.transform);
            instance.name = Locator.GeneratedTerrainPrefab.name;

            var generatedTerrain = instance.GetComponent<GeneratedTerrain>();
            generatedTerrain.Generate(settings);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
            throw new Exception($"Failed to generate for setting '{settings.Name}'", ex);
        }
    }

}
