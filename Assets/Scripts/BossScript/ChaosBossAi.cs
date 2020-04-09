using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosBossAi : MonoBehaviour
{
    private UiChaos ui;
    #region//General variables

    [Header("General",order = 0)]
    [SerializeField] private Transform target;
    private bool attackReady = false;
    [SerializeField] private float attackChance;
    [SerializeField]private int previousAttack = 0;
    [SerializeField]private int attackToDo = 0;
    public bool invincible;
    [SerializeField]private float hp;
    private static float maxHp = 200;
    private AudioSource audio;
    private static int nbAttack = 3;
    [SerializeField] private AudioClip[] clips;
    #endregion
    #region//Aoe variables
    [Header("Aoe Variables", order = 1)]
    [SerializeField] private float aoeCooldown;
    [SerializeField] private GameObject circle;
    [SerializeField]private float distanceBig = 2;
    #endregion

    #region //firewall variables
    [Header("Firewall Variables", order = 2)]
    [SerializeField] private float fireWallCooldown;
    [SerializeField] private int arraySize;
    [SerializeField] private float interval;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject parent;
    [SerializeField] private float distanceWall;
    private float[] array;
    private float firewallAB;
    private int noSpawn;
    private int wallAmount = 3;
    #endregion

    #region//meteor variables
    [Header("Meteor Variables", order = 3)]
    [SerializeField] private float meteorCooldown;
    [SerializeField] private GameObject meteor;
    [SerializeField] private float radius = 4;
    [SerializeField]private int meteorNumber = 10;
    private Vector2 position;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        firewallAB = Mathf.Ceil(arraySize * 0.5f);
        array = new float[arraySize];
        hp = maxHp;
        ui = UiChaos.instance;
        ui.HpUpdate(hp);
        Invoke("AttackCooldown", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackReady && Random.Range(0, 100) > attackChance)
        {
            do
            {
                attackToDo = Random.Range(0, nbAttack);
            } while (attackToDo == previousAttack);


            previousAttack = attackToDo;
                switch (attackToDo)
                {
                    case 0: AoeUnder(aoeCooldown, target);
                    
                        break;
                    case 1: StartCoroutine(FireWall(fireWallCooldown, target.position.x, wallAmount));
                        break;
                    case 2: Meteor(meteorCooldown, target.position);
                        break;
                }
            audio.PlayOneShot(clips[attackToDo]);
            attackReady = false;
        }
    }
    [SerializeField] private SpriteRenderer tookDamage;
    public void GetHit(float damage)
    {

        if (!invincible)
        {
            hp = hp - damage;
            ui.HpUpdate(hp);
            HpCheck();
            tookDamage.enabled = true;
            Invoke("RemoveDamage", 0.5f);
            audio.PlayOneShot(clips[nbAttack]);
        }
        else audio.PlayOneShot(clips[nbAttack + 1]);
        
    }
    private void RemoveDamage()
    {
        tookDamage.enabled = false;
    }
    private void HpCheck()
    {
        if (hp <= 0) ui.Die("LifeScene");
        //We can make different things happen here
    }
    
    private void AoeUnder(float cooldown, Transform lockedTarget)
    {
        Invoke("AttackCooldown", cooldown);
        GameObject aoeUnder;
        if (Vector2.Distance(transform.position, lockedTarget.position) < distanceBig)
        {
            aoeUnder = Instantiate(circle, transform.position, transform.rotation);
            aoeUnder.transform.localScale *= 3;
        }
        else
        {
            aoeUnder = Instantiate(circle, target.position, transform.rotation);
        }
    }
    private IEnumerator FireWall(float cooldown, float lockedTarget, int wallNumber)
    {
        GameObject p = Instantiate(parent, transform.position, transform.rotation);
        Invoke("AttackCooldown", cooldown);
        for (int i = 0; i < wallNumber; ++i)
        {
            noSpawn = Random.Range(0 + (int)Mathf.Ceil(firewallAB * 0.5f), arraySize - (int)Mathf.Floor(firewallAB * 0.5f));
            for (int i2 = 0; i2 < array.Length; ++i2)
            {
                array[i2] = 0 + firewallAB - (interval * i2);
                if (i2 != noSpawn)
                {
                    GameObject test = Instantiate(fire, new Vector2(28, array[i2]), Quaternion.identity);
                    test.transform.parent = p.transform;
                }
            }
            yield return new WaitForSecondsRealtime(distanceWall);
        }
    }
    private void Meteor(float cooldown, Vector2 lockedTarget)
    {
        Invoke("AttackCooldown", cooldown);
        for(int i = 0; i < meteorNumber; ++i)
        {
            do
            {
                position = new Vector2(lockedTarget.x + Random.Range(-radius, radius), lockedTarget.y + Random.Range(-radius, radius));
            } while (Vector2.Distance(position, lockedTarget) > radius);
            

            GameObject meteorInstance = Instantiate(meteor, position, Quaternion.identity);
        }

    }
    private void AttackCooldown()
    {
        attackReady = true;
    }
}
