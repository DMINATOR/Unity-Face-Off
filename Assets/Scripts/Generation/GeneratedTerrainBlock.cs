using System;
using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Generate(string name, int columnCount, int rowCount, int cellNumber)
    {
        this.name = name;
        var material = Locator.SpriteRenderer.material;
        var newMaterial = new Material(material);

        // Set shader values
        newMaterial.SetFloat(SHADER_COLUMN_COUNT, columnCount);
        newMaterial.SetFloat(SHADER_ROWS_COUNT, rowCount);
        newMaterial.SetFloat(SHADER_CELL_NUMBER, cellNumber);

        // Set new instance
        Locator.SpriteRenderer.material = newMaterial;
    }
}
