using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public bool isHost = false;
    public int id;
    public string username;
    public float health;
    public float maxHealth;
    public int itemCount = 0;

    public bool isSeeker = false;
    public static float timer;
    public Text timerText;

    public MeshRenderer playerModel;
    public MeshRenderer googleModel;
    public GameObject pistolModel;

    public PlayerController playerController;

    private Vector3 fromPos = Vector3.zero;
    private Vector3 toPos = Vector3.zero;
    private float lastTime;

    public void Initialize(int _id, string _username, bool _isHost)
    {
        id = _id;
        username = _username;
        health = maxHealth;
        isHost = _isHost;
    }
    public void SetHealth(float _health)
    {
        health = _health;
        if (health <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        playerModel.enabled = false;
        googleModel.enabled = false;
        pistolModel.SetActive(false);
    }
    public void Respawn()
    {
        playerModel.enabled = false;
        googleModel.enabled = false;
        pistolModel.SetActive(false);
        SetHealth(maxHealth);
    }

    public void SetHost()
    {
        Debug.Log("SetHost");
        isHost = true;
    }

    public bool IsHost()
    {
        return isHost;
    }
    private void Start()
    {
        timerText = GameObject.FindGameObjectWithTag("timerTXT").GetComponent<Text>();

    }
    private void Update()
    {
        GunEnable();
        this.transform.position = Vector3.Lerp(fromPos, toPos, (Time.time - lastTime) / (1.0f / 30));

        if (GameManager.players[Client.instance.myId].isSeeker)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ClientSend.PlayerShoot(playerController.camTransform.forward);
            }
        }
        else if (!GameManager.players[Client.instance.myId].isSeeker)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                ClientSend.PlayerThrowItem(playerController.camTransform.forward);
            }
        }


        timerText.text = timer.ToString("F1");


        if (Input.GetKeyDown(KeyCode.M))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void GunEnable()
    {
        if (isSeeker)
        {
            pistolModel.SetActive(true);
        }
        else
        {
            pistolModel.SetActive(false);
        }
    }
    public void SetPosition(Vector3 position)
    {

        fromPos = toPos;
        toPos = position;
        lastTime = Time.time;
    }
}
