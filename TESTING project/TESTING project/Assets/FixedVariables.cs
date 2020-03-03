using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedVariables : MonoBehaviour
{
    public static FixedVariables instance = null;
    private float timer;
    private string lastScene;
    public string LastScene { get => lastScene; set => lastScene = value; }
    public float Timer { get => timer; set => timer = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(TimeAdd());
    }

    // Update is called once per frame
    void Update()
    {
    }
    private IEnumerator TimeAdd()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            ++Timer;
        }
    }
}
