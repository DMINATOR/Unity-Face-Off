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
    [Tooltip("Number of columns to create for terrain")]
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
}
