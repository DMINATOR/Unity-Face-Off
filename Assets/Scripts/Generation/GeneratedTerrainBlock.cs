using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(GeneratedTerrainBlockLocator))]
public class GeneratedTerrainBlock : MonoBehaviour
{
    //Not exposed

    [Header("Constants")]

    [ReadOnly]
    [Tooltip("Shader Constant for column count")]
    public static string SHADER_COLUMN_COUNT = "sColumnCount";

    [ReadOnly]
    [Tooltip("Shader Constant for rows count")]
    public static string SHADER_ROWS_COUNT = "sRowsCount";

    [ReadOnly]
    [Tooltip("Shader Constant for cell number")]
    public static string SHADER_CELL_NUMBER = "sCellNumber";

    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public GeneratedTerrainBlockLocator Locator;

    [Header("Settings")]
    [ReadOnly]
    [Tooltip("Settings to load TERRAIN_GENERATION_PIXELS_PER_UNITY value")]
    public SettingsConstants.Name TERRAIN_GENERATION_PIXELS_PER_UNITY = SettingsConstants.Name.TERRAIN_GENERATION_PIXELS_PER_UNIT;


    [Header("Loaded Settings")]
    [ReadOnly]
    [Tooltip("Current size of the block for generation in Pixels (loaded from Settings)")]
    public int TerrainGenerationPixelsPerUnit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Generate(string name, int columnCount, int rowCount, int cellNumber, Sprite sprite, SortingLayer sortingLayer, bool generateColliders)
    {
        try
        {
            TerrainGenerationPixelsPerUnit = SettingsController.Instance.GetValue<int>(TERRAIN_GENERATION_PIXELS_PER_UNITY);

            this.name = name;

            Locator.PolygonCollider.enabled = generateColliders;
            Locator.RigidBody.simulated = generateColliders;

            Material newMaterial = new Material(Locator.SpriteRenderer.sharedMaterial);
            Locator.SpriteRenderer.sprite = sprite;
            Locator.SpriteRenderer.sortingLayerID = sortingLayer.id;

            // Set shader values
            newMaterial.SetFloat(SHADER_COLUMN_COUNT, columnCount);
            newMaterial.SetFloat(SHADER_ROWS_COUNT, rowCount);
            newMaterial.SetFloat(SHADER_CELL_NUMBER, cellNumber);

            // Set new instance
            Locator.SpriteRenderer.material = newMaterial;

            // Add it, after sprite is set
            if (generateColliders)
            {
                // Get pixel data
                var textureToModify = sprite.texture;
                var textureData = textureToModify.GetPixelData<Color>(0);
                var blockPixels = GetBlockPixels(textureData.ToArray(), TerrainGenerationPixelsPerUnit, columnCount, rowCount, cellNumber);

                var points = new Vector2[]
                {
                new Vector2( -0.3f, -0.3f ),
                new Vector2( 0.3f, -0.3f ),
                new Vector2( 0.3f, 0.3f ),
                new Vector2( -0.3f, 0.3f )
                };

                Locator.PolygonCollider.SetPath(0, points);
            }
        }
        catch(Exception ex)
        {
            throw new Exception($"Failed to generate for {nameof(columnCount)}:{columnCount},{nameof(rowCount)}:{rowCount},{nameof(cellNumber)}:{cellNumber}", ex);
        }
    }

    private Color[] GetBlockPixels(Color[] sourceTextureArray, int pixelsPerUnit, int columnsCount, int rowCount, int cellNumber)
    {
        var cellVolumeInPixels = pixelsPerUnit * pixelsPerUnit; // How many pixels one cell takes
        var destinationArray = new Color[cellVolumeInPixels];

        var cellIndexStartPos = cellNumber * pixelsPerUnit; // Start index position of our cell on the array

        for (var y = 0; y < pixelsPerUnit; y++)
        {
            for (var x = 0; x < pixelsPerUnit; x++)
            {
                // Pixel position relative to the start of a cell
                var pixelIndex = y * pixelsPerUnit + x;

                // Each vertical
                var cellOffsetPosition = ((columnsCount) * pixelsPerUnit) * y;

                // Pixel position relative to the start of a texture
                var pixelIndexInTexture = x + cellIndexStartPos + cellOffsetPosition;

                if( pixelIndex >= destinationArray.Length)
                {
                    throw new IndexOutOfRangeException($"pixelIndex ({pixelIndex}) >= destinationArray ({destinationArray.Length})");
                }

                if( pixelIndexInTexture >= sourceTextureArray.Length)
                {
                    throw new IndexOutOfRangeException($"pixelIndexInTexture ({pixelIndexInTexture}) >= sourceTextureArray ({sourceTextureArray.Length})");
                }

                // Copy pixel from source to destination
                destinationArray[pixelIndex] = sourceTextureArray[pixelIndexInTexture];
            }
        }

        return destinationArray;
    }
}
