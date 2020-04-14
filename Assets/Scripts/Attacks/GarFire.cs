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
    private bool vertical = false;
    private float max = 18;

    public bool Vertical { get => vertical; set => vertical = value; }

    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        sprite = GetComponent<SpriteRenderer>();
        Invoke("Activate", 2f);
        Invoke("Suicide", 3f);
        if (Vertical) max = 32;
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
            if (sprite.size.x >= max) activate = false;
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
