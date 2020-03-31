using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private static float maxHp = 1000;
    private float hp = maxHp;
    private int damageableBoss;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject[] healthPads;
    #region //Chaos
    private ChaosFinal cf;
    [SerializeField] private GameObject chaosPrefab1;
    [SerializeField] private GameObject chaosPrefab2;
    [SerializeField] private GameObject chaosPrefab2Parent;
    [SerializeField] private GameObject chaosPrefab3;
    #endregion
    #region //Life
    private LifeFinal lf;
    [SerializeField] private GameObject lifePrefab1;
    [SerializeField] private GameObject lifePrefab2;
    #endregion
    #region //Order
    private OrderFinal of;
    [SerializeField] private GameObject orderPrefab1;
    [SerializeField] private GameObject orderPrefab2;
    private float burstStartAngle = -20f;
    private int burstAmount = 3;
    private float burstDelay = 2f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        cf = ChaosFinal.Cf;
        lf = LifeFinal.Lf;
        of = OrderFinal.Of;
        StartCoroutine(BlockArea());
        ChangeBoss();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) cf.Meteor(target.position, chaosPrefab3, 20, 10f);
        if (Input.GetKeyDown(KeyCode.Alpha2)) StartCoroutine(cf.FireLine(target.position, chaosPrefab2, chaosPrefab2Parent, 3, 0, new float[30], 1));
        if (Input.GetKeyDown(KeyCode.Alpha3)) cf.AoeUnder(target.position, chaosPrefab1);
        if (Input.GetKeyDown(KeyCode.Alpha4)) StartCoroutine(OrderMissile());
        if (Input.GetKeyDown(KeyCode.Alpha5)) StartCoroutine(of.ConstantFiring(orderPrefab2));
        if (Input.GetKeyDown(KeyCode.Alpha6)) StartCoroutine(LifeHoming());
        if (Input.GetKeyDown(KeyCode.Alpha7)) StartCoroutine(lf.FireLine(target.position, lifePrefab2));
        
    }
    private IEnumerator OrderMissile()
    {
        while (true)
        {
            StartCoroutine(of.Missile(target.position, orderPrefab1, burstStartAngle, burstAmount));
            yield return new WaitForSeconds(burstDelay);
        }
    }
    private IEnumerator LifeHoming()
    {
        for(int i = 0; i < 7; ++i)
        {
            lf.IceShardTarget(target.position, lifePrefab1);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void ReduceHp(float damage)
    {
        hp -= damage;
        if(hp <= 0.9f * maxHp)
        {
            ChangeBoss();
        }
    }
    private void ChangeBoss()
    {
        int previous = damageableBoss;
        do
        {
            damageableBoss = Random.Range(0, 4);
        } while (previous == damageableBoss);
        switch (damageableBoss)
        {
            default:
                cf.Invincible = false;

                break;
        }
    }
    private void PlayerHeal()
    {
        healthPads[Random.Range(0, healthPads.Length)].SetActive(true);
    }
    private IEnumerator BlockArea()
    {
        do
        {
            yield return new WaitForSeconds(40);
            //block one of the 6 area 
        } while (hp > 0);
        
    }
}
