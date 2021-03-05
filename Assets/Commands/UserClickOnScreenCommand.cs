using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UserClickOnScreenCommand : ICommand
{
    Vector3 _mousePosition;
    Tilemap _tileMap;

    private Vector3Int previousPosition;

    /// <summary>
    /// Clicks on screen and selects tile from a map that was clicked on
    /// </summary>
    /// <param name="mousePosition">Input.mousePosition</param>
    /// <param name="tilemap">Tilemap to perform changes on</param>
    public UserClickOnScreenCommand(Vector3 mousePosition, Tilemap tilemap)
    {
        _mousePosition = mousePosition;
        _tileMap = tilemap;
    }

    public void Execute()
    {
        var worldPoint = Camera.main.ScreenToWorldPoint(_mousePosition);
        Debug.Log(string.Format($"Mouse {_mousePosition.x}, {_mousePosition.y} -> {worldPoint.x},{worldPoint.y}"));

        // Get a cell on a tile map
        Vector3Int currentPosition = _tileMap.WorldToCell(worldPoint);
        var currentTile = _tileMap.GetTile(currentPosition);

        Debug.Log($"Cell [{currentPosition}] -> Tile [{currentTile?.name}]");

        if( currentTile != null )
        {
            _tileMap.SetTile(currentPosition, null);
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
