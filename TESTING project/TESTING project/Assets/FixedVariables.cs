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
    public float StaminaUpgrade { get => staminaUpgrade; set => staminaUpgrade = value; }
    public float HealthUpgrade { get => healthUpgrade; set => healthUpgrade = value; }

    private float staminaUpgrade = 0;
    private float healthUpgrade = 0;

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
        StaminaUpgrade = 0;
        HealthUpgrade = 0;
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(TimeAdd());
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
