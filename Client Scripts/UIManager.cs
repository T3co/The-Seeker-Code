using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public bool gameStarted;

    public GameObject startMenu;
    public GameObject gameTitle;
    public GameObject mainMenu;
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject exitButton;
    public GameObject backButton;
    public GameObject timerText;
    public InputField usernameField;

    public GameObject startBTN;
    public InputField ipField;

    private Client client;
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
        Application.targetFrameRate = 120;
    }
    private void Start()
    {
        startBTN.SetActive(false);
        timerText.SetActive(false);

        client = Client.instance.gameObject.GetComponent<Client>();
    }
    public void ConnectToServer()
    {
        if (ipField.text == "")
        {
            return;
        }
        if (ipField.text == "lh")
        {
            Client.instance.ip = "127.0.0.1";
            ipField.text = Client.instance.ip;
        }
        if (ipField.text == "amit")
        {
            Client.instance.ip = "176.231.189.191";
            ipField.text = Client.instance.ip;
        }
        Client.instance.ip = ipField.text;
        Debug.Log(Client.instance.ip);

        startMenu.SetActive(false);
        gameTitle.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        usernameField.interactable = false;
        Client.instance.ConnectToServer();

        timerText.SetActive(true);
    }
    
    public void StartBTN()
    {
        gameStarted = true;
        ClientSend.StartGame();
    }
    public void Play()
    {
        mainMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void Back()
    {

    }

}
