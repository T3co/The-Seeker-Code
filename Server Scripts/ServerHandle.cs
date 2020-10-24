using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;

public class ServerHandle
{
    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();
        int onlinePlayer = _packet.ReadInt();
        bool _isHost = false;

        NetworkManager.instance.playersOnline += onlinePlayer;

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        if (_fromClient == 1)
        {
            _isHost = true;
        }
        Server.clients[_fromClient].SendIntoGame(_username, _isHost);


            
        
    }
    public static void PlayerMovement(int _fromClient, Packet _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }
        Quaternion _rotation = _packet.ReadQuaternion();

        Server.clients[_fromClient].player.SetInput(_inputs, _rotation);
    }
    public static void PlayerShoot(int _fromClient, Packet _packet)
    {
        Vector3 _shootDirection = _packet.ReadVector3();

        Server.clients[_fromClient].player.Shoot(_shootDirection);
    }
    public static void StartGame(int _fromClient, Packet _packet)
    {
        Vector3 loc = new Vector3(0f, 5f, 0f);

        bool timerOn = _packet.ReadBool();

        NetworkManager.instance.gameStarted = timerOn;

        GameTimer gameTimer = NetworkManager.instance.GetComponent<GameTimer>();



        gameTimer.enabled = timerOn;
        ServerSend.PlayerSeeker();

        NetworkManager.instance.MovePlayers(loc);
    }
    public static void PlayerThrowItem(int _fromClient, Packet _packet)
    {
        Vector3 _throwDirection = _packet.ReadVector3();

        Server.clients[_fromClient].player.ThrowItem(_throwDirection);
    }
}
