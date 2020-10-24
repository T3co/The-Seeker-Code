using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRules : MonoBehaviour
{
    public GameTimer gameTimer;
    public InputField timerField;
    void Update()
    {
        if (timerField.text != "")
        {
            float timer;
            timer = float.Parse(timerField.text);
            gameTimer.maxTimer = timer;
        }
    }
}
