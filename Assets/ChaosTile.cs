    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosTile : MonoBehaviour
{
    private GameManager code;
    private ChaosBossAi boss;
    private bool isTouching = false;
    [SerializeField] private float damageTick = 0.9f;
    [SerializeField] private float damageValue = 3f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTouching = true;
        StartCoroutine(ChaosTileDamage());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
    }

    private IEnumerator ChaosTileDamage()
    {
        while (isTouching && boss.invincible)
        {
            code.GetHit(damageValue);
            yield return new WaitForSecondsRealtime(damageTick);
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        boss = GameObject.Find("Boss-ChaosStage-1").GetComponent<ChaosBossAi>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
