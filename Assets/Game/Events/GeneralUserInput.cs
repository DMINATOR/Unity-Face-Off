using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneralUserInput : MonoBehaviour
{
    // Tilemap that will be affected
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var commandDown = new UserClickOnScreenCommand(Input.mousePosition, tilemap);
            commandDown.Execute();
        }
    }
}
