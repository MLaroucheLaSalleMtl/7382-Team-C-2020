using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss0Attacks : MonoBehaviour
{
    [SerializeField] private static GameObject fireball;

    public static void Attack(float attackID)
    {
        switch (attackID)
        {
            case 0:
                {
                    Fireball();
                }break;
        }
    }
    private static void Fireball()
    {
        //fireball = Instantiate(fireball, transform.position, transform.rotation);

        //fireball.GetComponent<Rigidbody>().AddForce(.position * 200f);
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
