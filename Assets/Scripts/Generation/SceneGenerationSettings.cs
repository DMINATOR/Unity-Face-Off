using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Settings to use for Scene generation
[System.Serializable]
public class SceneGenerationSettings
{
    [Tooltip("Generation Name")]
    public string Name;

    [Tooltip("Sprite to assign to Prefab material")]
    public Sprite Sprite;

    [Tooltip("Destination layer will be used as generation output")]
    public GameObject DestinationLayer;

    [Tooltip("Sorting Layer to set for rendering sprites")]
    public string SortingLayer;

    [Header("Options")]

    [Tooltip("Specifies to generate physics colliders for generated blocks")]
    public bool GeneratePhysicsColliders;

    [Tooltip("Number of columns to create for terrain")]
    public int ColumnsCount;

    [Tooltip("Number of rows to create for terrain")]
    public int RowsCount;

}
