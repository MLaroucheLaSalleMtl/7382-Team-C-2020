using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private GameManager code;
    private Vector2 direction;
    private static float speed = 3;
    private static float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(transform.position.x, transform.position.y - 10f);
        code = GameManager.instance;
        Invoke("Suicide", 8f);
    }
    private void Suicide()
    {
        Destroy(transform.parent.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, Time.deltaTime * speed);
        if(transform.position.y - direction.y <= 0.1f)
        {
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        code.GetHit(damage);
    }
}
