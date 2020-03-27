using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    private AsyncOperation async;
    private FixedVariables variables;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPaused = !isPaused;
        }
    }

    private void Start()
    {
        variables = FixedVariables.instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    public void Pause()
    {
        
    }

    public void Test()
    {
        Debug.Log("test");
    }
    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        
        Cursor.visible = true;
    }

    public void DeactivateMenu()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        isPaused = false;
        //Cursor.visible = false;
    }

    public void Menu(string sceneToLoad)
    {
        DeactivateMenu();

        if (async == null)
        {
            if (variables != null) variables.LastScene = "";
            PlayerPrefs.SetString("SceneToLoad", sceneToLoad);

            async = SceneManager.LoadSceneAsync("Loading");
            async.allowSceneActivation = true;
        }
    }
}