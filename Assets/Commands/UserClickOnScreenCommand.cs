using System;
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
            var sprite = _tileMap.GetSprite(currentPosition);
            var renderer = _tileMap.GetComponent<TilemapRenderer>();

            CreateOrUpdateRenderTexture(sprite, renderer);

            _tileMap.RefreshAllTiles();
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

    private void CreateOrUpdateRenderTexture(Sprite sprite, TilemapRenderer renderer)
    {
        if (renderer == null || renderer.material == null)
        {
            Debug.Log($"No renderer detected!");
        }

        var isMaskTextureCreated = renderer.material.GetFloat("MaskTextureCreated");
        var textureToModify = (Texture2D)sprite.texture;

        if (isMaskTextureCreated != 1.0f)
        {
            // Modify source texture
            Debug.Log($"Creating Modified texture...");

            if (textureToModify == null)
            {
                throw new Exception("No source texture detected!");
            }
            else
            {
                // Create new texture
                var newTextureToModify = new Texture2D(textureToModify.width, textureToModify.height, textureToModify.format, false, false)
                {
                    alphaIsTransparency = textureToModify.alphaIsTransparency,
                    anisoLevel = textureToModify.anisoLevel,
                    filterMode = textureToModify.filterMode,
                    hideFlags = textureToModify.hideFlags,
                    minimumMipmapLevel = textureToModify.minimumMipmapLevel,
                    mipMapBias = textureToModify.mipMapBias,
                    requestedMipmapLevel = textureToModify.requestedMipmapLevel,
                    wrapMode = textureToModify.wrapMode,
                    wrapModeU = textureToModify.wrapModeU,
                    wrapModeV = textureToModify.wrapModeV,
                    wrapModeW = textureToModify.wrapModeW
                };

                var textureData = textureToModify.GetPixelData<Color>(0);
                newTextureToModify.SetPixelData(textureData, 0);

                Debug.Log($"... created");

                renderer.material.SetTexture("_MainTex", newTextureToModify);
                renderer.material.mainTexture = newTextureToModify;

                renderer.material.SetFloat("MaskTextureCreated", 1.0f);

                textureToModify = newTextureToModify;
            }
        }
        else
        {
           textureToModify = (Texture2D)renderer.material.GetTexture("_MainTex");
        }

        // Modify texture here
        var pointX = sprite.rect.xMin;
        var pointY = sprite.rect.yMin;

        var previousColor = textureToModify.GetPixel((int)pointX, (int)pointY);
        var newColor = Color.white;

        Debug.Log($"{pointX},{pointY} [{previousColor}] -> [{newColor}]");

        for(var x = 0; x < sprite.rect.xMax; x++)
        {
            for(var y = 0; y < sprite.rect.yMax; y++)
            {
                textureToModify.SetPixel(
                    (int)(pointX + x),
                    (int)(pointY + y), 
                    newColor);
            }
        }

        textureToModify.Apply();
    }

    //private Texture2D GetTextureToModify(TilemapRenderer renderer)
    //{
    //    var isMaskTextureCreated = renderer.material.GetFloat("MaskTextureCreated"); 
    //    var textureToModify = (Texture2D)renderer.material.GetTexture("_MainTex");

    //    if (isMaskTextureCreated != 1.0f)
    //    {
    //        // Modify source texture
    //        Debug.Log($"Creating Modified texture...");

    //        if (textureToModify == null)
    //        {
    //            throw new Exception("No source texture detected!");
    //        }
    //        else
    //        {
    //            // Create new texture
    //            var newTextureToModify = new Texture2D(textureToModify.width, textureToModify.height, textureToModify.format, false, false)
    //            {
    //                alphaIsTransparency = textureToModify.alphaIsTransparency,
    //                anisoLevel = textureToModify.anisoLevel,
    //                filterMode = textureToModify.filterMode,
    //                hideFlags = textureToModify.hideFlags,
    //                minimumMipmapLevel = textureToModify.minimumMipmapLevel,
    //                mipMapBias = textureToModify.mipMapBias,
    //                requestedMipmapLevel = textureToModify.requestedMipmapLevel,
    //                wrapMode = textureToModify.wrapMode,
    //                wrapModeU = textureToModify.wrapModeU,
    //                wrapModeV = textureToModify.wrapModeV,
    //                wrapModeW = textureToModify.wrapModeW
    //            };

    //            var textureData = textureToModify.GetPixelData<Color>(0);
    //            newTextureToModify.SetPixelData(textureData, 0);

    //            Debug.Log($"... created");

    //            renderer.material.SetTexture("_MainTex", newTextureToModify);

    //            renderer.material.SetFloat("MaskTextureCreated", 1.0f);

    //            textureToModify = newTextureToModify;
    //        }
    //    }

    //    return textureToModify;
    //}
}
