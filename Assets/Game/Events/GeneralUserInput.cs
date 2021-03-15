using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneralUserInput : MonoBehaviour
{
    // Tilemap that will be affected
    public Tilemap tilemap;
    public Vector3 OldPosition;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = Input.mousePosition;

        // Trigger to show current position
        if( OldPosition != newPosition)
        {
            var commandMove = new UserMoveCursorCommand(Input.mousePosition, this.gameObject);
            commandMove.Execute();
        }

        // Trigger event to destroy tiles
        if (Input.GetMouseButtonDown(0))
        {
            var commandDown = new UserClickOnScreenCommand(Input.mousePosition, spriteRenderer.bounds, tilemap);
            commandDown.Execute();
        }
    }
}
