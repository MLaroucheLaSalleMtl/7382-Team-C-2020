using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBossAi : MonoBehaviour
{
    private UiChaos ui;
    #region//General
    [SerializeField] private Transform target;
    private bool attackReady = false;
    [SerializeField] private float attackChance;
    private int attackToDo = 0;
    private float previousAttack = -1;
    [SerializeField] private float hp;
    private static float maxHp = 200;
    [SerializeField] private SpriteRenderer tookDamage;
    private AudioSource audio;
    private static int nbAttack = 2;
    [SerializeField] private AudioClip[] clips;
    #endregion

    [SerializeField]private GameObject fireBall;
    [SerializeField] private GameObject circle;
    

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        hp = maxHp;
        ui = UiChaos.instance;
        Invoke("AttackCooldown", 2f);
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
                case 0:
                    AoeUnder(3, target);
                    break;
                case 1:
                    StartCoroutine(FireLine(3, target));
                    break;
            }
            attackReady = false;
            audio.PlayOneShot(clips[attackToDo]);
        }
    }

    public void GetHit(float damage)
    {
        hp = hp - damage;
        ui.HpUpdate(hp);
        HpCheck();
        tookDamage.enabled = true;
        Invoke("RemoveDamage", 0.5f);
        audio.PlayOneShot(clips[nbAttack]);
    }
    private void RemoveDamage()
    {
        tookDamage.enabled = false;
    }
    private void HpCheck()
    {
        if (hp <= 0) ui.Die("ChaosScene");
    }
    
    private void AoeUnder(float cooldown, Transform lockedTarget)
    {
        
        Invoke("AttackCooldown", cooldown);
        GameObject aoeUnder;
        if (Vector2.Distance(transform.position, lockedTarget.position) < 4.5f)
        {
            aoeUnder = Instantiate(circle, transform.position, transform.rotation);
            aoeUnder.transform.localScale *= 3.5f;
        }
        else
        {
            aoeUnder = Instantiate(circle, target.position, transform.rotation);
        }
    }
    //private void RangeFire(float cooldown)
    //{
    //    Invoke("AttackCooldown", cooldown);
    //    StartCoroutine(FireLine(target));
    //}
    private IEnumerator FireLine(float cooldown, Transform lockedTarget)
    {
        Invoke("AttackCooldown", cooldown);
        float vectorX = 0.9f * (lockedTarget.position.x - transform.position.x);
        float vectorY = 0.9f * (lockedTarget.position.y - transform.position.y);
        float incrementX = vectorX * 0.1f;
        float incrementY = vectorY * 0.1f;
        float fireballX = 0;
        float fireballY = 0;
        float n = 0;
        for(int i = 0; i < 500; ++i)
        {
            yield return new WaitForSeconds(0.04f);

            GameObject fuckkkkkkkkkkkkkkkk = Instantiate(fireBall, new Vector2(transform.position.x + fireballX, transform.position.y + fireballY), transform.rotation);
            fuckkkkkkkkkkkkkkkk.transform.Rotate(new Vector3(0, 0, 215));
            fireballX += incrementX;
            fireballY += incrementY;
            n += 1f;
        }
    }
    private void AttackCooldown()
    {
        attackReady = true;
    }
}
