using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Settings to use for Scene generation
[System.Serializable]
public class SceneGenerationSettings
{
    [Tooltip("Generation Name")]
    public string Name;

    [Tooltip("Source Texture to be used for generation")]
    public Texture2D SourceTexture;

    [Tooltip("Destination layer will be used as generation output")]
    public GameObject DestinationLayer;

    [Header("Options")]
    public bool GeneratePhysicsColliders;
}
