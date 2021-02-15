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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

            if (settings.SourceTexture == null)
            {
                throw new Exception("No source texture is provided!");
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
