using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private static void CreateSprite(string name, Vector2 position, int orderInLayer)
    {
        var pathString = $"Sprites/{name}";
        var sprite = Resources.Load<Sprite>(pathString);
        var go = new GameObject(name)
        {
            transform =
            {
                position = position
            }
        };
        go.tag = name;
        var spriteRenderer = go.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = orderInLayer;
    }

    private void Awake()
    {
        if (Camera.main != null) Camera.main.orthographicSize = 16f;
        CreateSprite("UFO", new Vector2(0, 0), 10);
        CreateSprite("Background", new Vector2(0, 0), 0);
        for (var i = 0; i < 12; i++)
        {
            var randomX = Random.Range(-11.5f, 11.5f);
            var randomY = Random.Range(-11.5f, 11.5f);
            CreateSprite("Pickup", new Vector2(randomX, randomY), 5);
            
        }

        var ufo = GameObject.FindWithTag("UFO");
        var ufoRb = ufo.AddComponent<Rigidbody2D>();
        ufoRb.gravityScale = 0;
        
        ufo.AddComponent<PlayerController>();
    }
}