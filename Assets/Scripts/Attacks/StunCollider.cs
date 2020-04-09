using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunCollider : MonoBehaviour
{
    private BossManager bm;
    [SerializeField] private bool isStun;
    private void Start()
    {
        bm = BossManager.Bm;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isStun) bm.Stun();
        else bm.Heal();
        gameObject.SetActive(false);
    }
    
}
