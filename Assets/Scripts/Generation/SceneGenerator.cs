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

            Debug.Log("Generation finished");
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
            throw new Exception("Generation failed", ex);
        }
    }

}
