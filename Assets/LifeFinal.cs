using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFinal : MonoBehaviour
{
    private static LifeFinal lf = null;
    private bool invincible = true;

    public static LifeFinal Lf { get => lf; set => lf = value; }
    public bool Invincible { get => invincible; set => invincible = value; }

    private void Awake()
    {
        if (Lf == null)
        {
            Lf = this;
        }
        else if (Lf != this)
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
