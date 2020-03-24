﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Upgrade : MonoBehaviour
{
    private FixedVariables variables;
    private AsyncOperation async;
    // Start is called before the first frame update
    void Start()
    {
        variables = FixedVariables.instance;
    }
    public void StaminaUpgrade()
    {
        ++variables.StaminaUpgrade;
        Invoke("Switch", 5f);
    }
    public void HealthUpgrade()
    {
        ++variables.HealthUpgrade;
        Invoke("Switch", 5f);
    }
    private void Switch()
    {
        ChangeScene(variables.SceneLoad);
    }
    private void ChangeScene(string sceneToLoad)
    {
        if (async == null)
        {
            PlayerPrefs.SetString("SceneToLoad", sceneToLoad);
            async = SceneManager.LoadSceneAsync("Loading");
            async.allowSceneActivation = true;
        }
    }
    
}
