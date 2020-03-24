using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    [SerializeField] private float damage;
    private GameManager code;
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        Invoke("Suicide", 10f);
    }
    private void Suicide()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

    }
    public void SetDirection(Vector2 _direction, float _speed)
    {
        direction = _direction;
        speed = _speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            code.GetHit(damage);
        }
    }
}
