using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMoveCursorCommand : ICommand
{
    Vector3 _mousePosition;
    GameObject _gameObject;

    public UserMoveCursorCommand(Vector3 mousePosition, GameObject gameObject)
    {
        _mousePosition = mousePosition;
        _gameObject = gameObject;
    }

    public void Execute()
    {
        var worldPoint = Camera.main.ScreenToWorldPoint(_mousePosition);

        _gameObject.transform.position = new Vector3(worldPoint.x, worldPoint.y);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
