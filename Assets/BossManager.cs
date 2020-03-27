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
    #endregion
    #region //Life
    private LifeFinal lf;
    #endregion
    #region //Order
    private OrderFinal of;
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
        if (Input.GetKeyDown(KeyCode.L)) cf.AoeUnder(target.position, chaosPrefab1);
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
