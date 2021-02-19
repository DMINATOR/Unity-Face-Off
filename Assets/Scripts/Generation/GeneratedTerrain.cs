using System;
using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(int columnsCount, int rowsCount)
    {
        TerrainGenerationPixelsPerUnit = SettingsController.Instance.GetValue<int>(TERRAIN_GENERATION_PIXELS_PER_UNITY);

        ColumnsCount = columnsCount;
        RowsCount = rowsCount;

        var xEdge = columnsCount / 2;
        var yEdge = rowsCount / 2;

        for(var y = 0; y < rowsCount; y++)
        {
            for (var x = 0; x < columnsCount; x++)
            {
                var position = new Vector3(x - xEdge, yEdge - y);

                GameObject instance = Instantiate(Locator.GeneratedTerrainBlockPrefab, position, Quaternion.identity, this.transform);
                instance.name = $"{x}:{y}";
            }
        }
    }
}
