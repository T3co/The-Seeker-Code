using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float timer;
    public float maxTimer;
    private void Start()
    {
        timer = maxTimer;
    }
    private void FixedUpdate()
    {
        ServerSend.Timer(timer);

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ServerSend.GameOver();
            timer = maxTimer;
            return;
        }
    }
}
