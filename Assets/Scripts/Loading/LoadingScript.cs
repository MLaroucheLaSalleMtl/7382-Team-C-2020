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
        //Cursor.visible = false;
        PlayerPrefs.DeleteKey("SceneToLoad");
        async = SceneManager.LoadSceneAsync(sceneToLoad);
        async.allowSceneActivation = false;
        variables = FixedVariables.instance;
        lore.text = "";
        loreZ.text = "";    
        
        ChooseText();
        Invoke("Test", 3f);
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
            lore.text = LoadingText.loreTexts[0];
            loreZ.text = LoadingText.loreTextsZ[0];//bridge before
            
        }
        if(variables.LastScene == "BridgeScene")
        {
            if (sceneToLoad == "BridgeScene")
            {
                lore.text = LoadingText.bridgeTips[Random.Range(0, LoadingText.bridgeTips.Length)];
                loreZ.text = LoadingText.bridgeTipsZ[Random.Range(0, LoadingText.bridgeTipsZ.Length)];//bridge  tips
            }
            else if(sceneToLoad != "ChaosScene")
            {
                //display bridge after
                lore.text = LoadingText.loreTexts[1];//bridge after
                loreZ.text = LoadingText.loreTextsZ[1];

            }
            else
            {
                lore.text = LoadingText.loreTexts[2];//chaos before
                lore.text = LoadingText.loreTexts[2];
            }
        }
        
        if(variables.LastScene == "ChaosScene")
        {
            if (sceneToLoad == "ChaosScene")
            {
                lore.text = LoadingText.chaosTips[Random.Range(0, LoadingText.chaosTips.Length)];
                loreZ.text = LoadingText.chaosTipsZ[Random.Range(0, LoadingText.chaosTipsZ.Length)];//chaos tips
            }
            else if(sceneToLoad != "LifeScene")
            {
                lore.text = LoadingText.loreTexts[3];
                loreZ.text = LoadingText.loreTextsZ[3];//chaos after
            }
            else
            {
                lore.text = LoadingText.loreTexts[4];
                loreZ.text = LoadingText.loreTextsZ[4];//life before
            }
        }

        if (variables.LastScene == "LifeScene")
        {
            if (sceneToLoad == "LifeScene")
            {
                lore.text = LoadingText.lifeTips[Random.Range(0, LoadingText.lifeTips.Length)];
                loreZ.text = LoadingText.lifeTipsZ[Random.Range(0, LoadingText.lifeTipsZ.Length)];//life tips
            }
            else if(sceneToLoad != "OrderScene")
            {
                lore.text = LoadingText.loreTexts[5];
                loreZ.text = LoadingText.loreTextsZ[5];//life after
            }
            else
            {
                lore.text = LoadingText.loreTexts[6];
                loreZ.text = LoadingText.loreTextsZ[6];//order before
            }
        }

        if (variables.LastScene == "OrderScene")
        {
            if (sceneToLoad == "OrderScene")
            {
                lore.text = LoadingText.orderTips[Random.Range(0, LoadingText.orderTips.Length)];
                loreZ.text = LoadingText.orderTipsZ[Random.Range(0, LoadingText.orderTipsZ.Length)];//order tips
            }
            else if(sceneToLoad != "FinalBoss")
            {
                lore.text = LoadingText.loreTexts[7];
                loreZ.text = LoadingText.loreTextsZ[7];//order after
            }
            else
            {
                lore.text = LoadingText.loreTexts[8];
                loreZ.text = LoadingText.loreTextsZ[8];//final before
            }
        }

        if (variables.LastScene == "FinalBoss")
        {
            if (sceneToLoad == "FinalBoss")
            {
                lore.text = LoadingText.finalTips[Random.Range(0, LoadingText.finalTips.Length)];
                loreZ.text = LoadingText.finalTipsZ[Random.Range(0, LoadingText.finalTipsZ.Length)];//final tips
            }
            else
            {
                lore.text = LoadingText.loreTexts[9];
                loreZ.text = LoadingText.loreTextsZ[9];//final after
            }
        }
               
        if (variables.LastScene == "WinScreen")
        {
            lore.text = LoadingText.congratsText;
            loreZ.text = LoadingText.congratsTextZ;//after win screen
        }
    }
}
