using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class VideoStream : MonoBehaviour
{
    private FixedVariables variables;
    private AsyncOperation async;
    public RawImage raw;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        variables = FixedVariables.instance;
        StartCoroutine(PlayVideo());
        Invoke("MainMenu", 40f);
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
    //    {
    //        MainMenu();
    //    }
    //}

    public void OnEnd(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MainMenu();
        }

    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        raw.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
    }
    private void MainMenu()
    {
        if (async == null)
        {
            variables.Timer = 0;
            if (variables != null) variables.LastScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("SceneToLoad", "MainMenu");

            async = SceneManager.LoadSceneAsync("Loading");
            async.allowSceneActivation = true;
        }
    }
}
