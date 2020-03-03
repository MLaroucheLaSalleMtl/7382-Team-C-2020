using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallScript : MonoBehaviour
{
    private Vector2 direction = new Vector2();
    [SerializeField] private float wallSpeed;
    [SerializeField] private float damage;
    private GameManager code;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(-1000, transform.position.y);
        code = GameManager.instance;
        Invoke("Die", 50f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, wallSpeed * Time.deltaTime);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        code.GetHit(damage);
    }
}
