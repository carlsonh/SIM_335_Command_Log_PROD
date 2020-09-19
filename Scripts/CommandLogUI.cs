using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class CommandLogUI : MonoBehaviour
{
    public static CommandLogUI instance;

    private void Awake()
    {
        // if an instance already exists and it's not this one - destroy us
        if (instance != null && instance != this)
            gameObject.SetActive(false);
        else
        {
            // set the instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



    public Text commandLogDisplay;


    public void UpdateCommandLogDisplay(string command)
    {
        commandLogDisplay.text += "\n" + command;
    }

}
