using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{

    //IsometricCharacterRenderer isoRenderer;

        // make a reference to the array and try to use direction thatway instead of hardcoding it

    private Rigidbody2D rigidbody;

    [SerializeField] private float dashSpeed;
     private float dashDuration;
    [SerializeField] private float startDashDuration;
    private int direction;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        dashDuration = startDashDuration;
    }

    private void Update()
    {
        if(direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                direction = 1;
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                direction = 2;
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                direction = 3;
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                direction = 4;
            }
        }
        else
        {
            if(dashDuration <= 0)
            {
                direction = 0;
                dashDuration = startDashDuration;
                rigidbody.velocity = Vector2.zero;
            }
            else
            {
                dashDuration -= Time.fixedDeltaTime;
                if(direction == 1)
                {
                    rigidbody.velocity = Vector2.left * dashSpeed;  
                }
                else if (direction == 2)
                {
                    rigidbody.velocity = Vector2.right * dashSpeed;
                }
                else if (direction == 3)
                {
                    rigidbody.velocity = Vector2.up * dashSpeed;
                }
                else if (direction == 4)
                {
                    rigidbody.velocity = Vector2.down * dashSpeed;
                }
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    float dashDistance = 1.5f;

        //    transform.position = newPos * dashDistance;
        //    //transform.position += newPos * dashDistance;
        //}
    }

}
