﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public int spawnerId;
    public bool hasItem;
    public MeshRenderer itemModel;

    public float itemRoatationSpeed = 50f;
    public float itemBobSpeed = 2f;

    private Vector3 basePosition;

    private void Update()
    {
            transform.Rotate(Vector3.up, itemBobSpeed * Time.deltaTime, Space.World);
            transform.position = basePosition + new Vector3(0f, 0.25f * Mathf.Sin(Time.time * itemBobSpeed), 0f);
    }
    public void Initialize(int _spawnerId, bool _hasItem)
    {
        spawnerId = _spawnerId;
        hasItem = _hasItem;
        itemModel.enabled = _hasItem;

        basePosition = transform.position;
    }
    public void ItemSpawned()
    {
        hasItem = true;
        itemModel.enabled = true;
    }
    public void ItemPickedUp()
    {
        hasItem = false;
        itemModel.enabled = false;
    }
}
