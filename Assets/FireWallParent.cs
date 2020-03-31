using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallParent : MonoBehaviour
{
    private Vector2 direction;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(-1000, transform.position.y);
        Invoke("Die", 50f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
