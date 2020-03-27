using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gtfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die", 5f);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
