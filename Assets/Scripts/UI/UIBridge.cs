using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBridge : MonoBehaviour
{
    public static UIBridge instance = null;
    private FixedVariables variables;
    [SerializeField]private Text timer;
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
        variables = FixedVariables.instance;
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = LoadingText.GetTime(variables.Timer);
    }
}
