using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeMeleeScript : MonoBehaviour
{
    private GameManager code;
    private SpriteRenderer sprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            code.GetHit(1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        sprite = GetComponent<SpriteRenderer>();
        Invoke("Seppuku", 3f);
        Invoke("Activate", 2f);
    }

    private void Activate()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        sprite.enabled = true;
    }
    private void Seppuku()
    {
        Destroy(gameObject);
    }
    
}
