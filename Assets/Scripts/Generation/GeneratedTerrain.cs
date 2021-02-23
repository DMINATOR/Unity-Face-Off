using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GeneratedTerrainLocator))]
public class GeneratedTerrain : MonoBehaviour
{
    //Not exposed


    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public GeneratedTerrainLocator Locator;

    [Header("Settings")]
    [ReadOnly]
    [Tooltip("Settings to load TERRAIN_GENERATION_PIXELS_PER_UNITY value")]
    public SettingsConstants.Name TERRAIN_GENERATION_PIXELS_PER_UNITY = SettingsConstants.Name.TERRAIN_GENERATION_PIXELS_PER_UNIT;


    [Header("Loaded Settings")]
    [ReadOnly]
    [Tooltip("Current size of the block for generation in Pixels (loaded from Settings)")]
    public int TerrainGenerationPixelsPerUnit;

    [Tooltip("Number of columns to create for terrain")]
    [ReadOnly]
    public int ColumnsCount;

    [ReadOnly]
    [Tooltip("Number of rows to create for terrain")]
    public int RowsCount;

    [ReadOnly]
    [Tooltip("Sprite to use for material")]
    public Sprite Sprite;

    [ReadOnly]
    [Tooltip("Sorting layer to set")]
    public SortingLayer SortingLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(SceneGenerationSettings settings)
    {
        TerrainGenerationPixelsPerUnit = SettingsController.Instance.GetValue<int>(TERRAIN_GENERATION_PIXELS_PER_UNITY);

        ColumnsCount = settings.ColumnsCount;
        RowsCount = settings.RowsCount;
        Sprite = settings.Sprite;
        SortingLayer = SortingLayer.layers.ToList().Where( l => l.name == settings.SortingLayer).Single();

        var xEdge = settings.ColumnsCount / 2;
        var yEdge = settings.RowsCount / 2;

        var cntr = 0;

        for(var y = 0; y < settings.RowsCount; y++)
        {
            for (var x = 0; x < settings.ColumnsCount; x++)
            {
                var position = new Vector3(x - xEdge, yEdge - y);

                GameObject instance = Instantiate(Locator.GeneratedTerrainBlockPrefab, position, Quaternion.identity, this.transform);
                var generaterTerrainBlock = instance.GetComponent<GeneratedTerrainBlock>();
                generaterTerrainBlock.Generate($"{x}:{y}", ColumnsCount, RowsCount, cntr, Sprite, SortingLayer, settings.GeneratePhysicsColliders);
                cntr++;
            }
        }
    }
}
