using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private void Awake()
    {
        var pickupCol = gameObject.AddComponent<CircleCollider2D>();
        pickupCol.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("UFO")) return;
        
        gameObject.SetActive(false);
    }
}