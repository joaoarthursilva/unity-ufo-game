using System;
using UnityEngine;
using System.Collections;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 13f;
    private Rigidbody2D _rb2d;
    private int _pickedUpAmount;
    private GameObject _textGO;
    private Text _text;
    private GameObject _winText;

    private void Awake()
    {
        gameObject.tag = "UFO";
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        var circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = 2;
        _pickedUpAmount = 0;
        _rb2d.angularDrag = 1.3f;
        Camera.main.gameObject.transform.parent = gameObject.transform;
        SetupText();
    }

    private void SetupText()
    {
        var counterCanvas = new GameObject
        {
            name = "counterCanvas",
            transform =
            {
                parent = gameObject.transform
            },
            layer = 5
        };
        var counterCanvasComponent = counterCanvas.AddComponent<Canvas>();
        counterCanvasComponent.worldCamera = Camera.main;

        counterCanvas.AddComponent<CanvasScaler>();
        counterCanvas.AddComponent<GraphicRaycaster>();
        _winText = new GameObject
        {
            name = "winText",
            transform =
            {
                position = new Vector3(0, 5f),
                localScale = new Vector3(.1f, .1f, .1f),
                parent = counterCanvas.transform
            }
        };
        _winText.AddComponent<CanvasRenderer>();
        var winTextComponent = _winText.AddComponent<Text>();
        winTextComponent.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        winTextComponent.alignment = TextAnchor.MiddleCenter;
        winTextComponent.color = Color.white;
        winTextComponent.text = "You win!";
        _winText.SetActive(false);

        _textGO = new GameObject
        {
            name = "counter",
            transform =
            {
                position = new Vector3(0, 3f),
                localScale = new Vector3(.1f, .1f, .1f),
                parent = counterCanvas.transform
            }
        };
        _textGO.AddComponent<CanvasRenderer>();
        _text = _textGO.AddComponent<Text>();
        _text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        _text.alignment = TextAnchor.MiddleCenter;
        _text.color = Color.yellow;
        _text.text = "Count Text";
    }

    private void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var movement = new Vector2(moveHorizontal, moveVertical);

        _rb2d.AddForce(movement * speed, ForceMode2D.Force);
        ManageText();
    }

    private void ManageText()
    {
        _text.text = "Count: " + _pickedUpAmount;
        if (_pickedUpAmount >= 12)
            _winText.SetActive(true);
    }

    public void PickUp()
    {
        _pickedUpAmount++;
    }
}