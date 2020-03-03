using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRangeScript : MonoBehaviour
{
    private GameManager code;
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            code.GetHit(damage);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        Invoke("Seppuku", 1f);
    }
    private void Seppuku()
    {
        Destroy(gameObject);
    }

}
