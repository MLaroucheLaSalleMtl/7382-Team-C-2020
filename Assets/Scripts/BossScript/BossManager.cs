using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private static BossManager bm;
    private GameManager code;
    private UiChaos ui;
    private static float maxHp = 1000;
    private float hp = maxHp;
    private float percentHp = 0.9f;
    private int damageableBoss;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject[] healthPads;
    [SerializeField] private GameObject[] stunPads;
    [SerializeField] private GameObject attack;
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
    private static float windForce = 180;
    private static int windLength = 200;
    #endregion
    #region //Order
    private OrderFinal of;
    [SerializeField] private GameObject orderPrefab1;
    [SerializeField] private GameObject orderPrefab2;
    private float burstStartAngle = -20f;
    private int burstAmount = 3;
    private float burstDelay = 2f;

    public static BossManager Bm { get => bm; set => bm = value; }
    #endregion

    private void Awake()
    {
        if (Bm == null)
        {
            Bm = this;
        }
        else if (Bm != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        cf = ChaosFinal.Cf;
        lf = LifeFinal.Lf;
        of = OrderFinal.Of;
        code = GameManager.instance;
        ui = UiChaos.instance;
        StartCoroutine(BlockArea());
        ChangeBoss();
        Pad(true);
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
        if (Input.GetKeyDown(KeyCode.Alpha8)) StartCoroutine(lf.Wind(target.position, target.GetComponent<Rigidbody2D>(), windForce, windLength));
        if (Input.GetKeyDown(KeyCode.P)) Stun();
        
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
        ui.HpUpdate(hp);
        if(hp <= percentHp * maxHp)
        {
            ChangeBoss();
            if(percentHp == 0.8f || percentHp == 0.6f || percentHp == 0.4f)
            {
                Pad(true);
                Pad(false);
            }
            percentHp -= 0.1f;
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
    private void Pad(bool isStun)
    {
        int rand;
        if (isStun)
        {
            rand = Random.Range(0, stunPads.Length);
            stunPads[rand].SetActive(true);
            StartCoroutine(PadGone(stunPads[rand], 20));
        }
        else
        {
            rand = Random.Range(0, healthPads.Length);
            healthPads[rand].SetActive(true);
            StartCoroutine(PadGone(healthPads[rand], 20));
        }
    }
    private IEnumerator PadGone(GameObject pad, float delay)
    {
        yield return new WaitForSeconds(delay);
        pad.SetActive(false);
    }
    public void ToParent(GameObject g)
    {
        g.transform.parent = attack.transform;
    }
    private void DestroyAllAttacks()
    {
        attack.BroadcastMessage("Suicide", SendMessageOptions.DontRequireReceiver);
    }
    public void Heal()
    {
        code.GetHit(-1);//works as a heal
    }
    public void Stun()
    {
        cf.AttackReady = false;
        lf.AttackReady = false;
        of.AttackReady = false;
        DestroyAllAttacks();
        Invoke("UnStun", 5f);
    }
    private void UnStun()
    {
        cf.AttackReady = true;
        lf.AttackReady = true;
        of.AttackReady = true;
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
