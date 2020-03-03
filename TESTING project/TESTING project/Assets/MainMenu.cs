using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]private Text timer;
    private AsyncOperation async;
    private FixedVariables variables;
    void Start()
    {
        variables = FixedVariables.instance;
    }
    private void Update()
    {
        timer.text = LoadingText.GetTime(variables.Timer);
    }
    public void SceneLoad(string scene)
        {
            if (async == null)
            {
            if(variables != null)
            {
                variables.LastScene = SceneManager.GetActiveScene().name;
            }
            
            PlayerPrefs.SetString("SceneToLoad", scene);
            async = SceneManager.LoadSceneAsync("Loading");
            async.allowSceneActivation = true;

            }
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
