using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGeneratorLocator : MonoBehaviour
{
    [Header("Prefabs")]
    [Tooltip("Prefab to use for generating terrain")]
    public GameObject GeneratedTerrainPrefab;

    [Header("Generation settings")]

    [Tooltip("Generation Settings")]
    public SceneGenerationSettings[] Settings;


}
