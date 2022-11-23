using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private static GameObject CreateSprite(string name, Vector2 position, int orderInLayer)
    {
        var pathString = $"Sprites/{name}";
        var sprite = Resources.Load<Sprite>(pathString);
        var go = new GameObject(name)
        {
            transform =
            {
                position = position
            },
        };
        var spriteRenderer = go.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = orderInLayer;
        return go;
    }

    private static void CreateBackground()
    {
        var go = CreateSprite("Background", new Vector2(), -1);

        var collider = go.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2(14.1f, 0);
        collider.size = new Vector2(3f, 32);

        collider = go.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2(-14.1f, 0);
        collider.size = new Vector2(3f, 32);

        collider = go.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2(0, 14.1f);
        collider.size = new Vector2(32, 3f);

        collider = go.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2(0, -14.1f);
        collider.size = new Vector2(32, 3f);
    }

    private static void CreateUfo()
    {
        var ufoGo = CreateSprite("UFO", new Vector2(), 0);
        ufoGo.AddComponent<Rotator>();
        var ufoParentGO = new GameObject();
        ufoGo.transform.parent = ufoParentGO.transform;
        var ufoRb = ufoParentGO.AddComponent<Rigidbody2D>();
        ufoParentGO.AddComponent<PlayerController>();
        ufoRb.gravityScale = 0;
    }

    private static void CreatePickups()
    {
        var originalVector = new Vector2(10.85f, 0);

        for (var i = 0; i < 12; i++)
        {
            var rotationVector = Quaternion.Euler(0, 0, i * 30);
            Vector2 finalPosition = rotationVector * originalVector;
            var go = CreateSprite("Pickup", finalPosition, 0);
            go.AddComponent<PickupController>();
            go.AddComponent<Rotator>();
        }
    }


    private void Awake()
    {
        if (Camera.main != null) Camera.main.orthographicSize = 16f;
        CreateBackground();
        CreateUfo();
        CreatePickups();
    }
}