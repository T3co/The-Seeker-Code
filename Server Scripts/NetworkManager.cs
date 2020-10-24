using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    public int playersOnline;

    public GameObject playerPrefab;
    public GameObject projectilePrefab;
    public bool gameStarted = false;
    public int seekerID;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        Server.Start(50, 26050);
    }
    private void Update()
    {
        if (Server.clients.Count == 0)
        {
            return;
        }
        else
        {

            foreach (Client _cliet in Server.clients.Values)
            {
                if (_cliet.player.id == seekerID)
                {
                    if (_cliet.player.isDead)
                    {
                        ServerSend.GameOver();
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
    private void OnApplicationQuit()
    {
        Server.Stop();
    }
    public Player InstantiatePlayer()
    {
        return Instantiate(playerPrefab, new Vector3(85f, 5f, 0f), Quaternion.identity).GetComponent<Player>();
    }
    public void MovePlayers(Vector3 location)
    {
        if (Server.clients.Count != 0)
        {
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    _client.player.health = _client.player.maxHealth;
                    _client.player.itemAmount = 0;
                    _client.player.MoveToPos(location);
                }
            }
        }
    }
    public Projectile InstantiateProjectile(Transform _shootOrigin)
    {
        return Instantiate(projectilePrefab, _shootOrigin.position + _shootOrigin.forward * 0.7f, Quaternion.identity).GetComponent<Projectile>();
    }
}
