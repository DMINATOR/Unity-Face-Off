using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGeneratorLocator : MonoBehaviour
{
    [Header("Background Layers")]

    [Tooltip("Background Layer 1")]
    public GameObject BackgroundLayer1;

    [Tooltip("Background Layer 2")]
    public GameObject BackgroundLayer2;

    [Tooltip("Background Layer 3")]
    public GameObject BackgroundLayer3;


    [Header("Foreground Layers")]

    [Tooltip("Foreground Layer 1")]
    public GameObject ForegroundLayer1;

    [Tooltip("Foreground Layer 2")]
    public GameObject ForegroundLayer2;

    [Tooltip("Foreground Layer 3")]
    public GameObject ForegroundLayer3;
}
