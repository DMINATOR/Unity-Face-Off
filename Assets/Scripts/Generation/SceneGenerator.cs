using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

            if (settings.SourceMaterial == null)
            {
                throw new Exception("No source material is provided!");
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
            generatedTerrain.Generate(settings.ColumnsCount, settings.RowsCount);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
            throw new Exception($"Failed to generate for setting '{settings.Name}'", ex);
        }
    }

}
