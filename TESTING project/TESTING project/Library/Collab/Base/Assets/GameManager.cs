using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int maxHp = 100;
    private float maxLives = 3;
    [SerializeField]private float currentHp;
    [SerializeField]private float currentLives;
    private float maxStamina = 50;
    public float currentStamina;
    [SerializeField] private float meleeAttack = 10;
    //private float meleeStrongAttack = 35;
    private float typeAdvantage = 1.5f;
    private float typeDisadvantage = 0.5f;
    public HealthBar healthBar;
    [SerializeField] private float staminaRegen;


    private AsyncOperation async;
    private FixedVariables variables;
    private UiChaos ui;
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
        ui = UiChaos.instance;
        currentHp = maxHp;
        ui.PlayerHp(currentHp);
        currentLives = maxLives;
        variables = FixedVariables.instance;
        currentStamina = maxStamina;
    }
    private bool isAttacking = false;

    public float MeleeAttack { get => meleeAttack; set => meleeAttack = value; }

    public void Attack()
    {
        currentStamina -= maxStamina * 0.25f;
        isAttacking = true;
        Invoke("ResetAttack", 1f);
    }
    private void ResetAttack()
    {
        isAttacking = false;
    }
    // Update is called once per frame
    void Update()
    {
        //Attack(); // this is not necessary with the input system now implemented
        HpCheck();
        if(currentStamina <= maxStamina && !isAttacking)
        {
            currentStamina += Time.deltaTime * staminaRegen;
        }
        ui.Stamina(currentStamina);
    }
    
    private void HpCheck()
    {
        if (currentHp <= 0)
        {
            if(async == null)
            {
                if(variables != null) variables.LastScene = SceneManager.GetActiveScene().name;
                PlayerPrefs.SetString("SceneToLoad", SceneManager.GetActiveScene().name);
                
                async = SceneManager.LoadSceneAsync("Loading");
                async.allowSceneActivation = true;
            }
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
        ui.PlayerHp(currentHp);
    }
}
