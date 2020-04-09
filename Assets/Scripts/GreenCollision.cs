using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCollision : MonoBehaviour
{
    private GreenProtection green;
    [SerializeField] private SpriteRenderer knob;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        knob.enabled = true;
        green.Collision();
        GetComponent<BoxCollider2D>().enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        green = GreenProtection.instance;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
