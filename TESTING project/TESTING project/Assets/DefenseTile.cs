using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTile : MonoBehaviour
{
    [SerializeField] private float defense;
    private LifeBossAI boss;
    private LifeTileGone a;
    private void OnTriggerStay2D(Collider2D collision)
    {
        defense -= Time.deltaTime;
        if (defense < 0) DoSomething();
    }
    private void DoSomething()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        boss.Stun();
        a.Swap();
        Destroy(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("BossLightStage").GetComponent<LifeBossAI>();
        a = LifeTileGone.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
