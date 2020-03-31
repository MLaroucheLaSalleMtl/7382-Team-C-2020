using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShardHoming : MonoBehaviour
{
    private GameManager code;
    //[SerializeField]private float angle;
    //[SerializeField] private Transform endVector;
    //[SerializeField] private Transform startVector;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField]private Vector3 destination;
    private bool enabled = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            code.GetHit(damage);
        }
    }

    public void Shoot(Vector2 target)
    {
        enabled = true;
        destination = target;
    }

    // Start is called before the first frame update
    void Start()
    {

        code = GameManager.instance;
    }
    
    private void Update()
    {
        if(enabled)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if(transform.position == destination) { Destroy(gameObject); }
        }
        //angle = life.Angle(-3.681f, -4.236f, life.Target.position.x - transform.position.x, life.Target.position.y - transform.position.y);
        //if(life.D(endVector.position.x - startVector.position.x, endVector.position.y - startVector.position.y,life.Target,transform) < 0)
        //{
        //    angle = -angle;
        //}
        //transform.Rotate(0, 0, angle);
    }
}
