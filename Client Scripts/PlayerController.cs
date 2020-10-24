using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 100f;
    [SerializeField]
    private float clampAngle = 85f;
    
    private float verticalRotation;
    private float horizontalRotation;

    public Transform camTransform;

    public Camera myCam;
    private void FixedUpdate() 
    {
        SendInputToServer();


        
    }
    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)
        };

        ClientSend.PlayerMovement(_inputs);
    }

    private void Start()
    {
        verticalRotation = transform.localEulerAngles.x;
        horizontalRotation = transform.eulerAngles.y;
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        
            float _mouseVertical = -Input.GetAxis("Mouse Y");
            float _mouseHorizontal = Input.GetAxis("Mouse X");

            verticalRotation += _mouseVertical * sensitivity * Time.deltaTime;
            horizontalRotation += _mouseHorizontal * sensitivity * Time.deltaTime;

            verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

            myCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        
    }
}
