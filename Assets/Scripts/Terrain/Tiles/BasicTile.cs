using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BasicTile : Tile
{
    public Sprite SpriteOverride;

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        Debug.Log($"Sprite override triggered");
        tileData.sprite = SpriteOverride;
    }

    //public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    //{
    //    int mask = HasRoadTile(tilemap, location + new Vector3Int(0, 1, 0)) ? 1 : 0;
    //    mask += HasRoadTile(tilemap, location + new Vector3Int(1, 0, 0)) ? 2 : 0;
    //    mask += HasRoadTile(tilemap, location + new Vector3Int(0, -1, 0)) ? 4 : 0;
    //    mask += HasRoadTile(tilemap, location + new Vector3Int(-1, 0, 0)) ? 8 : 0;
    //    int index = GetIndex((byte)mask);
    //    if (index >= 0 && index < m_Sprites.Length)
    //    {
    //        tileData.sprite = m_Sprites[index];
    //        tileData.color = Color.white;
    //        var m = tileData.transform;
    //        m.SetTRS(Vector3.zero, GetRotation((byte)mask), Vector3.one);
    //        tileData.transform = m;
    //        tileData.flags = TileFlags.LockTransform;
    //        tileData.colliderType = ColliderType.None;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Not enough sprites in RoadTile instance");
    //    }
    //}

}
