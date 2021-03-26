using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UserClickOnScreenCommand : ICommand
{
    Vector3 _mousePosition;
    Tilemap _tileMap;
    Bounds _bounds;

    private Vector3Int previousPosition;

    /// <summary>
    /// Clicks on screen and selects tile from a map that was clicked on
    /// </summary>
    /// <param name="mousePosition">Input.mousePosition</param>
    /// <param name="tilemap">Tilemap to perform changes on</param>
    public UserClickOnScreenCommand(Vector3 mousePosition, Bounds bounds, Tilemap tilemap)
    {
        _mousePosition = mousePosition;
        _tileMap = tilemap;
        _bounds = bounds;
    }

    private List<Vector3Int> GetTilePositions(Bounds bounds)
    {
        var topLeftTileCell = _tileMap.WorldToCell(new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y));
        var bottomRightTileCell = _tileMap.WorldToCell(new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y));

        var results = new HashSet<Vector3Int>();

        for (var x = topLeftTileCell.x; x <= bottomRightTileCell.x; x++)
        {
            for (var y = topLeftTileCell.y; y <= bottomRightTileCell.y; y++)
            {
                var position = new Vector3Int(x, y, 0);
                var tile = _tileMap.GetTile(position);
                if ( tile != null )
                {
                    results.Add(position);
                }
            }
        }

        return results.ToList();
    }

    public void Execute()
    {
        var worldPoint = Camera.main.ScreenToWorldPoint(_mousePosition);

        var tilePositions = GetTilePositions(_bounds);
        //var tiles = _tileMap.GetTilesBlock(tileBounds);

        Debug.Log(string.Format($"Mouse {_mousePosition.x}; {_mousePosition.y} -> {worldPoint.x}; {worldPoint.y}"));
        //Debug.Log($"Bounds {_bounds} -> {tileBounds}");
        Debug.Log($"Tiles: {tilePositions}");

        ShowSelectedTiles(tilePositions);

        // Get a cell on a tile map
        //Vector3Int currentPosition = _tileMap.WorldToCell(worldPoint);
        //var currentTile = _tileMap.GetTile(currentPosition);

        //Debug.Log($"Cell [{currentPosition}] -> Tile [{currentTile?.name}]");

        //if( currentTile != null )
        //{
        //    var sprite = _tileMap.GetSprite(currentPosition);
        //    //var renderer = _tileMap.GetComponent<TilemapRenderer>();
        //    //var collider = _tileMap.GetComponent<TilemapCollider2D>();

        //    var newSprite = CreateNewSprite(sprite);
        //    var newTile = ScriptableObject.CreateInstance<BasicTile>();

        //    newTile.sprite = newSprite;

        //    _tileMap.SetTile(currentPosition, newTile);


        //    //collider.ProcessTilemapChanges();
        //}
    }

    private void ShowSelectedTiles(List<Vector3Int> tilePositions)
    {
        foreach(var tilePosition in tilePositions)
        {
            ShowSelectedTile(tilePosition);
        }
    }

    private void ShowSelectedTile(Vector3Int tilePosition)
    {
        var tile = _tileMap.GetTile(tilePosition);
        var sprite = _tileMap.GetSprite(tilePosition);

        var tilemapCollider = _tileMap.GetComponent<TilemapCollider2D>();
        var tilemapRenderer = _tileMap.GetComponent<TilemapRenderer>();
        var newSprite = CreateNewSpriteAndCutCircleAreaOut(sprite, tilePosition, false);
        var newTile = ScriptableObject.CreateInstance<BasicTile>();

        //var texture = newSprite.texture;
        //TextureScale.Bilinear(texture, 512, 512);

        //newSprite.texture.Resize(512, 512, newSprite.texture.format, false);
        //newSprite.texture.Apply();

        //var shape = new List<Vector2>();
        //var physicsShape = newSprite.GetPhysicsShape(0, shape);
        //TextureScale.Bilinear(texture, 128, 128);
        //var shapes = new List<Vector2[]>();
        //shapes.Add(shape.ToArray());
        //newSprite.OverridePhysicsShape(shapes);

        //newSprite.texture.Resize(128, 128);
        //var shapes = new List<Vector2[]>();
        //shapes.Add(shape.ToArray());
        //newSprite.OverridePhysicsShape(shapes);

        //TextureScale.Bilinear(newSprite.texture, 512, 512);
        //var vertices512 = newSprite.vertices;
        //TextureScale.Bilinear(newSprite.texture, 128, 128);
        //var vertices128 = newSprite.vertices;

        //newSprite.OverridePhysicsShape(new List<Vector2[]> {
        //new Vector2[] { new Vector2(0, 0), new Vector2(newSprite.rect.width, 0), new Vector2(newSprite.rect.width, newSprite.rect.height), new Vector2(0, newSprite.rect.height) },
        //});
        TraceSprite(newSprite);
        newTile.sprite = newSprite;
 
        _tileMap.SetTile(tilePosition, newTile);
    }

    public void TraceSprite(Sprite sprite)
    {
        var edge = 0.02f;

        var gapLength = 3U; //  "How much difference in pixels in a straight line is considered a gap. This can help smooth out the outline a bit."
        var product = 1f; //  "Product for optimizing the outline based on angle. 1 means no optimization. This value should be kept pretty high if you want to maintain round shapes. Note that some points (e.g. outer angles) are never optimized."
        var tolerance = 0.0f; // A higher value results in a simpler line (less points). A positive value close to zero results in a line with little to no reduction. A value of zero or less has no effect.
        ContourTracer tracer = new ContourTracer();
        Texture2D targetTex = sprite.texture;
        tracer.Trace(targetTex, Vector2Int.zero + new Vector2(-edge, -edge), 1 + edge, gapLength, product);

        var path = new List<Vector2>();
        var points = new List<Vector2>();
        var pointsList = new List<Vector2[]>();

        for (int i = 0; i < tracer.pathCount; i++)
        {
            tracer.GetPath(i, ref path);
            LineUtility.Simplify(path, tolerance, points);

            Debug.Log($"Points = {string.Join(",", points)}");

            if( points.Count > 2 )
            {
                pointsList.Add(points.ToArray());
            }
        }

        sprite.OverridePhysicsShape(pointsList);

        //var polygonCollider2D = gameObject.GetComponent<PolygonCollider2D>();
        //if (!polygonCollider2D) polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();

        //polygonCollider2D.pathCount = tracer.pathCount;
        //for (int i = 0; i < polygonCollider2D.pathCount; i++)
        //{
        //    tracer.GetPath(i, ref path);
        //    LineUtility.Simplify(path, tolerance, points);
        //    if (points.Count < 3)
        //    {
        //        polygonCollider2D.pathCount--;
        //        i--;
        //    }
        //    else
        //    {
        //        polygonCollider2D.SetPath(i, points);
        //    }
        //}
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

    private Sprite CreateNewSpriteAndCutCircleAreaOut(Sprite sprite, Vector3Int tilePosition, bool randomColor)
    {
        Texture2D tex = sprite.texture;

        var tex2 = new Texture2D((int)sprite.pixelsPerUnit, (int)sprite.pixelsPerUnit, tex.format, false, false)
        {
            alphaIsTransparency = tex.alphaIsTransparency,
            anisoLevel = tex.anisoLevel,
            filterMode = tex.filterMode,
            hideFlags = tex.hideFlags,
            minimumMipmapLevel = tex.minimumMipmapLevel,
            mipMapBias = tex.mipMapBias,
            requestedMipmapLevel = tex.requestedMipmapLevel,
            wrapMode = tex.wrapMode,
            wrapModeU = tex.wrapModeU,
            wrapModeV = tex.wrapModeV,
            wrapModeW = tex.wrapModeW
        };

        var newColor = new Color(
            UnityEngine.Random.Range(0.2f, 1),
            UnityEngine.Random.Range(0.2f, 1),
            UnityEngine.Random.Range(0.2f, 1),
            1.0f);

        var tileWorldPosition = _tileMap.CellToWorld(tilePosition);
        var centerCircleRadius = _bounds.extents.x;

        var multX = 1.0f / tex2.width;
        var multY = 1.0f / tex2.height;

        // Get texture rect from sprite as is
        var sourceTextureRect = sprite.textureRect;

        // If spite texture rect is new one, then use a smaller portion instead
        if( tex.width == (int)sprite.pixelsPerUnit)
        {
            sourceTextureRect = new Rect(0, 0, tex2.width, tex2.height);
        }

        for (var x = 0; x < tex2.width; x++)
        {
            for (var y = 0; y < tex2.height; y++)
            {
                var color = tex.GetPixel((int)(x + sourceTextureRect.x), (int)(y + sourceTextureRect.y));

                if (color.a <= 0.01f)
                {
                    // Clear the area
                    tex2.SetPixel(x, y, new Color(0, 0, 0, 0));
                    continue;
                }

                var localPoint = new Vector3(multX * x, multY * y);
                var worldPosition = localPoint + tileWorldPosition;

                if( _bounds.Contains(worldPosition))
                {
                    var distance = Vector3.Distance(_bounds.center, worldPosition);

                    // Only if we are within a circle magnitude
                    if ( distance <= centerCircleRadius)
                    {
                        // Clear the area
                        tex2.SetPixel(x, y, new Color(0,0,0,0));
                        continue;
                    }
                }

                if( randomColor )
                {
                    // Random color
                    tex2.SetPixel(x, y, newColor);
                }
                else
                {
                    // Color from the source
                    tex2.SetPixel(x, y, color);
                }
            }
        }

        tex2.Apply();

        var newTextureRect = new Rect(0, 0, tex2.width, tex2.height);
        var newSprite = Sprite.Create(tex2, newTextureRect, new Vector2(0.5f, 0.5f), sprite.pixelsPerUnit);

        return newSprite;
    }

    private Sprite CreateNewSprite(Sprite sprite)
    {
        Texture2D tex = sprite.texture;
        var tex2 = new Texture2D((int)sprite.pixelsPerUnit, (int)sprite.pixelsPerUnit);

        var newColor = new Color(
            UnityEngine.Random.Range(0.2f, 1),
            UnityEngine.Random.Range(0.2f, 1),
            UnityEngine.Random.Range(0.2f, 1),
            1.0f);

        for (var x = 0; x < tex2.width; x++)
        {
            for (var y = 0; y < tex2.height; y++)
            {
                var color = tex.GetPixel((int)(x + sprite.textureRect.x), (int)(y + sprite.textureRect.y));
                color.a = 1.0f;
                //if( color.a >= 0.8f )
                //{
                //    color = Color.black;
                //}

                tex2.SetPixel(x, y, newColor);
            }
        }

        tex2.Apply();

        var newSprite = Sprite.Create(tex2, new Rect(0, 0, tex2.width, tex2.height), new Vector2(0.5f, 0.5f), sprite.pixelsPerUnit);

        return newSprite;
    }

    private Sprite CreateNewSpriteTexture(Sprite sprite, TilemapRenderer renderer)
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

        //for (var x = 0; x < sprite.rect.xMax; x++)
        //{
        //    for (var y = 0; y < sprite.rect.yMax; y++)
        //    {
        //        textureToModify.SetPixel(
        //            (int)(pointX + x),
        //            (int)(pointY + y),
        //            newColor);
        //    }
        //}

        //for (var x = 0; x < textureToModify.width; x++)
        //{
        //    for (var y = 0; y < textureToModify.height; y++)
        //    {
        //        textureToModify.SetPixel(
        //            (int)(pointX + x),
        //            (int)(pointY + y),
        //            newColor);
        //    }
        //}

        textureToModify.Apply();
       
        var newSprite = Sprite.Create(textureToModify, sprite.rect, sprite.pivot, sprite.pixelsPerUnit, 0, SpriteMeshType.Tight, sprite.border);

        return newSprite;
        //return sprite;
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

        for (var x = 0; x < sprite.rect.xMax; x++)
        {
            for (var y = 0; y < sprite.rect.yMax; y++)
            {
                textureToModify.SetPixel(
                    (int)(pointX + x),
                    (int)(pointY + y),
                    newColor);
            }
        }

        textureToModify.Apply();
    }
}
