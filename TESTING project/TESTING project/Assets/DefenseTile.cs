using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseTile : MonoBehaviour
{
    private const float maxDefense = 15;
    private float defense;
    [SerializeField] private Image defenseBar;
    private LifeBossAI boss;
    private LifeTileGone a;
    private void OnTriggerStay2D(Collider2D collision)
    {
        defense -= Time.deltaTime;
        defenseBar.fillAmount = defense / maxDefense;
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
        defense = maxDefense;
        boss = GameObject.Find("BossLightStage").GetComponent<LifeBossAI>();
        a = LifeTileGone.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
