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
    private int percentHp = 9;
    private int damageableBoss = -1;
    private static float stunTime = 10f;
    private int blockedArea = -1;
    private int attackAmount = 0;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject[] healthPads;
    [SerializeField] private GameObject[] stunPads;
    [SerializeField] private GameObject[] blockedAreas;
    [SerializeField] private GameObject attackHolder;//parent that will contain all the attacks
    [SerializeField] private AudioClip[] clips;
    private AudioSource audio;
    #region //Chaos
    private ChaosFinal cf;
    [SerializeField] private GameObject chaosPrefab1;
    [SerializeField] private GameObject chaosPrefab2;
    [SerializeField] private GameObject chaosPrefab2Parent;
    [SerializeField] private GameObject chaosPrefab3;
    private int chaosRandom;
    #endregion
    #region //Life
    private LifeFinal lf;
    [SerializeField] private GameObject lifePrefab1;
    [SerializeField] private GameObject lifePrefab2;
    private static float windForce = 150;
    private static int windLength = 200;
    private int lifeRandom;
    #endregion
    #region //Order
    private OrderFinal of;
    [SerializeField] private GameObject orderPrefab1;
    [SerializeField] private GameObject orderPrefab2;
    [SerializeField] private GameObject orderPrefab3;
    private float burstStartAngle = -20f;
    private int burstAmount = 3;
    private float burstDelay;
    private int orderRandom;


    #endregion
    public static BossManager Bm { get => bm; set => bm = value; }
    public int AttackAmount { get => attackAmount; set => attackAmount = value; }

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
        StartCoroutine(ChaosClock());
        StartCoroutine(LifeClock());
        StartCoroutine(OrderMissile());
        StartCoroutine(of.ConstantFiring(orderPrefab2));
        StartCoroutine(of.GargoyleFire(orderPrefab3));

        ChangeBoss();
        Pad(true);
        audio = GetComponent<AudioSource>();
    }
    private IEnumerator ChaosClock()
    {
        while(hp >= 0)
        {
            yield return new WaitForSeconds(Random.Range(5, 8));//forces a delay before the next attack by that boss
            if (hp >= 0 )
            {
                while (AttackAmount >= 4)
                {
                    yield return new WaitForEndOfFrame();
                }
                AttackAmount++;
                chaosRandom = Random.Range(0, 100);
                if (chaosRandom < 20)
                {
                    AttackAmount++;
                    StartCoroutine(cf.FireLine(target.position, chaosPrefab2, chaosPrefab2Parent, 3, 0, 1));
                    Invoke("AttackDecrease", 20f);
                    Invoke("AttackDecrease", 10f);
                }
                else if (chaosRandom >= 20 && chaosRandom < 55)
                {
                    cf.AoeUnder(target.position, chaosPrefab1);
                    Invoke("AttackDecrease", 1.5f);
                }
                else {
                    cf.Meteor(target.position, chaosPrefab3, 20, 10f);
                    Invoke("AttackDecrease", 8f);
                }
                
            }
        }
    }
    private IEnumerator LifeClock()
    {
        while (hp >= 0) 
        {
            yield return new WaitForSeconds(Random.Range(5, 8));
            if (hp >= 0)
            {
                while (AttackAmount >= 4)
                {
                    yield return new WaitForEndOfFrame();
                }
                AttackAmount++;
                lifeRandom = Random.Range(0, 100);

                if (lifeRandom < 20)
                {
                    StartCoroutine(lf.Wind(target.position, target.GetComponent<Rigidbody2D>(), windForce, windLength, true));
                    Invoke("AttackDecrease", 2);
                }
                else if (lifeRandom >= 20 && lifeRandom < 60)
                {
                    StartCoroutine(LifeHoming());
                    Invoke("AttackDecrease", 4f);
                }
                else {
                    StartCoroutine(lf.FireLine(target.position, lifePrefab2));
                    Invoke("AttackDecrease", 2f);
                } 

            }
        }
    }
    private void AttackDecrease()
    {
        AttackAmount--;
    }
    private IEnumerator LifeHoming()
    {
        for(int i = 0; i < 7; ++i)
        {
            lf.IceShardTarget(target.position, lifePrefab1);
            yield return new WaitForSeconds(0.5f);
        }
    }//I want to update the player position for those attacks
    private IEnumerator OrderMissile()
    {
        while (hp >= 0)
        {
            while (attackAmount >= 4) yield return new WaitForEndOfFrame();
            StartCoroutine(of.Missile(target.position, orderPrefab1, burstStartAngle, burstAmount));
            AttackAmount++;
            Invoke("AttackDecrease", 5f);
            burstDelay = Random.Range(7, 10);
            yield return new WaitForSeconds(burstDelay);
        }
    }
    

    public void ReduceHp(float damage)
    {
        if (damage == 0) audio.PlayOneShot(clips[1]);
        else audio.PlayOneShot(clips[0]);

        hp -= damage;
        ui.HpUpdate(hp);
        if(hp <= Mathf.Round(((float)percentHp/10) * maxHp))//every 10% of the boss hp, the damageable boss changes. at every 20%, a stun pad and health pad appear to help the player
            //I had percent hp as a float but they are retarded and had hidden decimals 
        {
            ChangeBoss();
            if(percentHp == 8 || (percentHp) == 6 || (percentHp) == 4)
            {

                Pad(true);
                Pad(false);
            }
            if(Mathf.FloorToInt(hp) <= 0)//dead
            {
                Stun();
                audio.PlayOneShot(clips[3]);
                Invoke("DeadWait", 8f);//gives time for the death sfx to play
                for (int i = 0; i < blockedAreas.Length; i++) blockedAreas[i].SetActive(false);
            }
            percentHp = percentHp - 1;
        }
    }//function is called from the colliders
    public IEnumerator HitEffect(SpriteRenderer s, int counter)
    {
        s.enabled = true;
        counter++;
        yield return new WaitForSeconds(0.5f);
        counter = BridgeBossAi.RemoveDamage(counter, s);
        if (counter == 0) s.enabled = false;
    }
    private void DeadWait()
    {
        ui.Die("WinScreen");
    }
    private void ChangeBoss()//only one of the three bosses will be damageable at once
    {
        int previous = damageableBoss;
        do
        {
            damageableBoss = Random.Range(0, 3);
        } while (previous == damageableBoss);
        cf.Switch(false);
        lf.Switch(false);
        of.Switch(false);
        if (damageableBoss == 0) cf.Switch(true);
        if (damageableBoss == 1) lf.Switch(true);
        if (damageableBoss == 2) of.Switch(true);
        //cf.Invincible = true;
        //lf.Invincible = true;
        //of.Invincible = true;
        //if (damageableBoss == 0) cf.Invincible = false;
        //if (damageableBoss == 1) lf.Invincible = false;
        //if (damageableBoss == 2) of.Invincible = false;
    }

    private void Pad(bool isStun)
    {
        int rand;
        if (isStun)
        {
            rand = Random.Range(0, stunPads.Length);
            stunPads[rand].SetActive(true);
        }
        else
        {
            rand = Random.Range(0, healthPads.Length);
            healthPads[rand].SetActive(true);
        }
    }

    public void ToParent(GameObject g)
    {
        g.transform.parent = attackHolder.transform;
    }//puts the attack inside of an object, this way all of the attacks can be stopped
    private void DestroyAllAttacks()
    {
        attackHolder.BroadcastMessage("Suicide", SendMessageOptions.DontRequireReceiver);
        AttackAmount = 0;
        CancelInvoke("AttackDecrease");
    }

    public void Heal()
    {
        code.GetHit(-1);//works as a heal
    }
    public void Stun()
    {
        audio.PlayOneShot(clips[2]);
        cf.AttackReady = false;
        lf.AttackReady = false;
        of.AttackReady = false;
        DestroyAllAttacks();
        if (Mathf.Round(hp) >= 0) Invoke("UnStun", stunTime);

    }//All the boss stop attacking for a certain period of time
    private void UnStun()
    {
        cf.AttackReady = true;
        lf.AttackReady = true;
        of.AttackReady = true;
    }

    private IEnumerator BlockArea()
    {
        int previous = blockedArea;
        while(hp >= 0)
        {
            do
            {
                blockedArea = Random.Range(0, blockedAreas.Length);
            } while (previous == blockedArea);
            
            previous = blockedArea;
            blockedAreas[blockedArea].SetActive(true);

            yield return new WaitForSeconds(30);
            blockedAreas[blockedArea].SetActive(false);
        }
    }
}
