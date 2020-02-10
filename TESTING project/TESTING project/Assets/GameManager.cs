using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float maxHp = 10;
    private float maxLives = 3;
    private float currentHp;
    private float currentLives;
    private float meleeLightAttack = 20;
    private float meleeStrongAttack = 35;
    private float typeAdvantage = 1.5f;
    private float typeDisadvantage = 0.5f;
    private float rangeLightAttack = 15;
    private float rangeStrongAttack = 20;
    private float rangeShieldDamage = 10;
    private float rangeStrongShieldDamage = 20;

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
        currentLives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        //Attack(); // this is not necessary with the input system now implemented
        HpCheck();
        
    }
    //private void Attack() // this is not necessary with the input system now implemented
    //{
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        //do light attack
    //        Debug.Log("Light Melee Attack");
    //    }
    //    if (Input.GetButtonDown("Fire2"))
    //    {
    //        //do strong attack
    //        Debug.Log("Strong Melee Attack");
    //    }
    //}
    private void HpCheck()
    {
        if (currentHp <= 0)
        {
            HpReset();
        }
        if (currentLives <= 0)
        {
            Restart();
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
}
