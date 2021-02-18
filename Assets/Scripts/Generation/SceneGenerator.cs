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

    [Header("Settings")]
    [ReadOnly]
    [Tooltip("Settings to load TERRAIN_GENERATION_PIXELS_PER_UNITY value")]
    public SettingsConstants.Name TERRAIN_GENERATION_PIXELS_PER_UNITY = SettingsConstants.Name.TERRAIN_GENERATION_PIXELS_PER_UNIT;


    [Header("Loaded Settings")]
    [ReadOnly]
    [Tooltip("Current size of the block for generation in Pixels (loaded from Settings)")]
    public int TerrainGenerationPixelsPerUnit;

    private void Awake()
    {
        TerrainGenerationPixelsPerUnit = SettingsController.Instance.GetValue<int>(TERRAIN_GENERATION_PIXELS_PER_UNITY);
    }


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
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
            throw new Exception($"Failed to generate for setting '{settings.Name}'", ex);
        }
    }

}
