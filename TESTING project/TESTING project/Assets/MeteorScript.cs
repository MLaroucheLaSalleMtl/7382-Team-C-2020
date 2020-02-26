using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private GameManager code;
    private Vector2 direction;
    private float speed = 3;
    [SerializeField]private float damage;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(transform.position.x, transform.position.y - 10f);
        code = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, Time.deltaTime * speed);
        if(direction.y - transform.position.y <= 0.2f)
        {
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        code.GetHit(damage);
    }
}
