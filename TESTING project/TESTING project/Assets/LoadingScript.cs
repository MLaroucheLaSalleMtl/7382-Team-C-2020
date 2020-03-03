using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class LoadingScript : MonoBehaviour
{
    private FixedVariables variables;
    private AsyncOperation async;
    private string sceneToLoad;
    [SerializeField] private Text lore;
    [SerializeField] private Text loreZ;
    [SerializeField] private Text textContinue;
    [SerializeField] private Text textContinueZ;
    [SerializeField] private Text timer;
    // Start is called before the first frame update
    
    void Start()
    {
        //Cursor.lockState = CursorLockMode.None;
        sceneToLoad = PlayerPrefs.GetString("SceneToLoad", "MainMenu");
        //if (sceneToLoad != "MainMenu") Cursor.lockState = CursorLockMode.Locked;
        //else Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        PlayerPrefs.DeleteKey("SceneToLoad");
        async = SceneManager.LoadSceneAsync(sceneToLoad);
        async.allowSceneActivation = false;
        variables = FixedVariables.instance;
        lore.text = "";
        loreZ.text = "";    
        
        ChooseText();
        Invoke("Test", 5f);
    }
    private bool test = false;
    private void Test()
    {
        test = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (test && async.progress > 0.89 && SplashScreen.isFinished)
        {
            textContinue.enabled = true;
            textContinueZ.enabled = true;
            if (Input.anyKeyDown) { async.allowSceneActivation = true; }
        }
        timer.text = LoadingText.GetTime(variables.Timer);
    }
    private void ChooseText()
    {
        if(variables.LastScene == "MainMenu")
        {
            lore.text = LoadingText.loreText[0];
            loreZ.text = LoadingText.loreTextZ[0];
            
        }
        if(variables.LastScene == "BridgeScene")
        {
            if (sceneToLoad == "BridgeScene")
            {
                lore.text = LoadingText.bridgeTips[Random.Range(0, LoadingText.bridgeTips.Length)];
                loreZ.text = LoadingText.bridgeTipsZ[Random.Range(0, LoadingText.bridgeTips.Length)];
            }
            else
            {
                lore.text = LoadingText.loreText[1];
                loreZ.text = LoadingText.loreTextZ[1];

            }
        }
        
        if(variables.LastScene == "ChaosScene")
        {
            if (sceneToLoad == "ChaosScene")
            {
                lore.text = LoadingText.chaosTips[Random.Range(0, LoadingText.chaosTips.Length)];
                loreZ.text = LoadingText.chaosTipsZ[Random.Range(0, LoadingText.bridgeTips.Length)];
            }
            else
            {
                lore.text = LoadingText.loreText[2];
                loreZ.text = LoadingText.loreTextZ[2];
            }
        }
        if(variables.LastScene == "WinScreen")
        {
            lore.text = LoadingText.congratsText;
            loreZ.text = LoadingText.congratsTextZ;
        }
    }
}
