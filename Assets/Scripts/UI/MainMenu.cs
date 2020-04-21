using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField]private Text timer;
    private AsyncOperation async;
    private FixedVariables variables;
    public GameObject controls;
    public GameObject menu;
    public EventSystem eventSystem;
    public GameObject returnBtn;
    public GameObject startBtn;
    public GameObject levelSelect;
    public GameObject returnLvlBtn;
    public GameObject options;
    public GameObject returnOptions;

    void Start()
    {
        variables = FixedVariables.instance;
        variables.HealthUpgrade = 0;
        variables.StaminaUpgrade = 0;//resets upgrades
        controls.SetActive(false);
        Cursor.visible = true;
    }
    private void Update()
    {
        timer.text = LoadingText.GetTime(variables.Timer);
    }
    public void SceneLoad(string scene)
    {
        if (async == null)
        {
            if (variables != null)
            {
                variables.LastScene = SceneManager.GetActiveScene().name;
                if (scene != "BridgeScene") variables.Warp = true;
                else variables.Warp = false;
            }

            PlayerPrefs.SetString("SceneToLoad", scene);
            async = SceneManager.LoadSceneAsync("Loading");
            async.allowSceneActivation = true;

        }
    }

    private void OnEnable()
    {
        eventSystem = EventSystem.current;
    }

    public void Controls()
    {
        controls.SetActive(true);
        menu.SetActive(false);
        eventSystem.SetSelectedGameObject(returnBtn);
    }

    public void Return()
    {
        controls.SetActive(false);
        menu.SetActive(true);
        eventSystem.SetSelectedGameObject(startBtn);
    }
    public void ReturnLevel()
    {
        levelSelect.SetActive(false);
        menu.SetActive(true);
        eventSystem.SetSelectedGameObject(startBtn);
    }

    public void LevelSelect()
    {
        levelSelect.SetActive(true);
        menu.SetActive(false);
        eventSystem.SetSelectedGameObject(returnLvlBtn);
    }

    public void Options()
    {
        options.SetActive(true);
        menu.SetActive(false);
        eventSystem.SetSelectedGameObject(returnOptions);
    }

    public void ReturnOptions()
    {
        options.SetActive(false);
        menu.SetActive(true);
        eventSystem.SetSelectedGameObject(startBtn);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
