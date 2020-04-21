using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTeleport : MonoBehaviour
{
    [SerializeField]private float timeSpentClose = 0;
    private bool isInside = false;
    private LifeBossAI boss;
    private void Start()
    {
        boss = transform.parent.GetComponent<LifeBossAI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isInside = true;
            StartCoroutine(Tick());
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInside = false;
        timeSpentClose = timeSpentClose * 0.5f;
    }
    private IEnumerator Tick()
    {
        while (isInside)
        {
            yield return new WaitForSeconds(1f);
            ++timeSpentClose;
            if(timeSpentClose >= 8 && !boss.AttackReady)
            {
                while (boss.Stun1) yield return new WaitForEndOfFrame();
                timeSpentClose = 0;
                boss.Spawn();
            }
        }
    }
}
