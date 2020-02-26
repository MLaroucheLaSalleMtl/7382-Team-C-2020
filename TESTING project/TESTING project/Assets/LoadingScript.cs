using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class LoadingScript : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField] private Text lore;
    // Start is called before the first frame update
    void Start()
    {
        async = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("SceneToLoad", "Menu"));
        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
