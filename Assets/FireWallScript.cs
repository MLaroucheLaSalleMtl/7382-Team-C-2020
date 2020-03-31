using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallScript : MonoBehaviour
{
    [SerializeField] private float damage;
    private GameManager code;
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            code.GetHit(damage);
        }
        
    }
}
