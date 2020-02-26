using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeMeleeScript : MonoBehaviour
{
    private GameManager code;
    private SpriteRenderer[] sprite;
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            code.GetHit(damage);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        sprite = GetComponentsInChildren<SpriteRenderer>();
        Invoke("Seppuku", 3f);
        Invoke("Activate", 2f);
    }

    private void Activate()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        foreach(SpriteRenderer aaaa in sprite)
        {
            aaaa.enabled = true;
        }
    }
    private void Seppuku()
    {
        Destroy(gameObject);
    }
    
}
