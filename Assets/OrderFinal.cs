using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFinal : MonoBehaviour
{
    private static OrderFinal of = null;
    private bool invincible = true;

    public static OrderFinal Of { get => of; set => of = value; }
    public bool Invincible { get => invincible; set => invincible = value; }

    private void Awake()
    {
        if (Of == null)
        {
            Of = this;
        }
        else if (Of != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
