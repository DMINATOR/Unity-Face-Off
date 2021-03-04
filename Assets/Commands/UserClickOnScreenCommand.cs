using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UserClickOnScreenCommand : ICommand
{
    Vector3 _mousePosition;
    Tilemap _tileMap;

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
        Vector3Int coordinate = _tileMap.WorldToCell(worldPoint);
        var tile = _tileMap.GetTile(coordinate);

        Debug.Log($"Cell [{coordinate}] -> Tile [{tile?.name}]");
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
