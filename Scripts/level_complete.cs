﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_complete : MonoBehaviour
{
    // Start is called before the first frame update
    public static level_complete instance;

    private void Awake() {
        instance = this; //lazy
    }
    public void LoadNextLevel()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}