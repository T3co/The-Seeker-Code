using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float health;
    public float maxHealth;
    public int itemCount = 0;

    public bool isSeeker = false;
    public static bool isHost = false;
    public static float timer;
    public Text timerText;

    public MeshRenderer playerModel;
    public MeshRenderer googleModel;
    public GameObject pistolModel;

    public PlayerController playerController;

    private Vector3 fromPos = Vector3.zero;
    private Vector3 toPos = Vector3.zero;
    private float lastTime;

    public GameObject startBTN;
    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;
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

        GameManager.instance.startButton();
    }
    private void Update()
    {
        if (GameManager.players[Client.instance.myId].isSeeker)
        {

            this.gameObject.tag = "Seeker";
            pistolModel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ClientSend.PlayerShoot(playerController.camTransform.forward);
            }
        }
        else if (!GameManager.players[Client.instance.myId].isSeeker)
        {
            this.gameObject.tag = "Player";
            pistolModel.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("Right Click");
                ClientSend.PlayerThrowItem(playerController.camTransform.forward);
            }
        }

        timerText.text = timer.ToString("F1");
        //Mouse 
        if (Input.GetKeyDown(KeyCode.M))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (!GameManager.players[Client.instance.myId].isSeeker)
        {
            pistolModel.SetActive(false);
        }

        this.transform.position = Vector3.Lerp(fromPos, toPos, (Time.time - lastTime) / (1.0f / 30));
    }
    public void SetPosition(Vector3 position)
    {
        fromPos = toPos;
        toPos = position;
        lastTime = Time.time;
    }
}
