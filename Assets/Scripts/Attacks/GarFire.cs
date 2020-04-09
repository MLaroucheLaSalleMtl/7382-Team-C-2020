using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarFire : MonoBehaviour
{
    private GameManager code;
    private SpriteRenderer sprite;
    private bool activate = false;
    private Vector2 increment = new Vector2(0.6f, 0);
    [SerializeField] private float damage;
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        sprite = GetComponent<SpriteRenderer>();
        Invoke("Activate", 2f);
        Invoke("Suicide", 3f);
    }
    private void Activate()
    {
        activate = true;
    }
    private void Suicide()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            sprite.size += increment;
            if (sprite.size.x >= 18) activate = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            code.GetHit(damage);
        }
    }
}
