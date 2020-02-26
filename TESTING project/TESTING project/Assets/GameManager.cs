using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int maxHp = 100;
    private float maxLives = 3;
    [SerializeField]private float currentHp;
    [SerializeField]private float currentLives;
    private float maxStamina = 50;
    [SerializeField] private float currentStamina;
    private float meleeAttack = 20;
    //private float meleeStrongAttack = 35;
    private float typeAdvantage = 1.5f;
    private float typeDisadvantage = 0.5f;
    public HealthBar healthBar;

    private float time;
    [SerializeField] private Text text;
    
    //private float rangeLightAttack = 15;
    //private float rangeStrongAttack = 20;
    //private float rangeShieldDamage = 10;
    //private float rangeStrongShieldDamage = 20;


    public static GameManager instance = null;

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
        currentHp = maxHp;
        //healthBar.SetMaxHealth(maxHp);
        currentLives = maxLives;
        time = PlayerPrefs.GetFloat("Timer", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Attack(); // this is not necessary with the input system now implemented
        HpCheck();
        time += Time.deltaTime;
        text.text = GetTime(time);
        if (Input.GetKeyDown(KeyCode.T)) { time += 60; }
        
    }
    private static string GetTime(float timeInSeconds)
    {
        int minutes = ((int)timeInSeconds) / 60;
        int seconds = ((int)timeInSeconds) % 60;

        return minutes + ":" + ((seconds < 10) ? "0" + seconds : seconds.ToString());
    }


    private void HpCheck()
    {
        if (currentHp <= 0)
        {
            HpReset();
        }
        if (currentLives <= 0)
        {
            //Restart();
        }
    }
    private void HpReset()
    {
        --currentLives;
        currentHp = maxHp;
    }
    private void Restart()
    {
        Debug.Log("Scene Restart");
    }
    public void GetHit(float damage)
    {
        currentHp -= damage;

    }
}
