using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 13f;
    private Collider2D _col2d;
    private Rigidbody2D _rb2d;
    private int _pickedUpAmount;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _col2d = gameObject.AddComponent<CircleCollider2D>();
        _pickedUpAmount = 0;
    }

    private void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");

        var moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector2(moveHorizontal, moveVertical);

        _rb2d.AddForce(movement * speed);
    }

    public void PickUp()
    {
        _pickedUpAmount += 1;
    }
}