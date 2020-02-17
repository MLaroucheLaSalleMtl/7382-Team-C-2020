using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBossAi : MonoBehaviour
{
    [SerializeField]private Transform target;
    private bool attackReady = true;
    [SerializeField] private float attackChance;

    [SerializeField]private GameObject fireBall;
    [SerializeField] private GameObject circle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (attackReady)
        {
            if(Random.Range(0,100) > attackChance)
            {
                attackReady = false;
                RangeFire(3);
                AoeUnder(3, target);
            }
        }
    }
    private void AoeUnder(float cooldown, Transform lockedTarget)
    {
        
        Invoke("AttackCooldown", cooldown);
        GameObject aoeUnder;
        if (Vector2.Distance(transform.position, lockedTarget.position) < 10)
        {
            aoeUnder = Instantiate(circle, transform.position, transform.rotation);
            aoeUnder.transform.localScale *= 3;
        }
        else
        {
            aoeUnder = Instantiate(circle, target.position, transform.rotation);
        }
    }
    private void RangeFire(float cooldown)
    {
        Invoke("AttackCooldown", cooldown);
        StartCoroutine(FireLine(target));
    }
    private IEnumerator FireLine(Transform lockedTarget)
    { 
        float vectorX = 0.9f * (lockedTarget.position.x - transform.position.x);
        float vectorY = 0.9f * (lockedTarget.position.y - transform.position.y);
        float incrementX = vectorX * 0.1f;
        float incrementY = vectorY * 0.1f;
        float fireballX = 0;
        float fireballY = 0;
        float n = 0;
        for(int i = 0; i < 500; ++i)
        {
            yield return new WaitForSecondsRealtime(0.04f);

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
