using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float dashLength;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;
    private IsometricCharacterRenderer isoRenderer;
    private InputSystem inputSystem;
    private Rigidbody2D rbody;
    private Vector2 movement = new Vector2();
    private void Awake()

    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        
    }
    private bool isDashing = false;
    private bool dashRecover = true;
    private float horizontalInput;
    private float verticalInput;
    private Vector2 inputVector = new Vector2();
    private Vector2 currentPos = new Vector2();
    // Update is called once per frame
    private void FixedUpdate()
    {
        currentPos = rbody.position; // reference to the position of the character based on rigid body


        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && dashRecover)
        {
            isDashing = true;
            dashRecover = false;
            Invoke("DashFreeze", dashLength);
            Invoke("DashCooldown", dashCooldown);
            inputVector = Vector2.ClampMagnitude(inputVector, 1); // limit the magnitude thus preventing diagonal movement being faster than cardinal movement 
            movement = dashSpeed * inputVector;

        }
        else if (!isDashing)
        {
            horizontalInput = Input.GetAxis("Horizontal"); //get the input
            verticalInput = Input.GetAxis("Vertical");
            inputVector = new Vector2(horizontalInput, verticalInput); // store the input in a new vector 2 | the teacher said he doesnt want this , we have to use the new method
            inputVector = Vector2.ClampMagnitude(inputVector, 1); // limit the magnitude thus preventing diagonal movement being faster than cardinal movement 
            movement = inputVector * movementSpeed; // this makes it so for that frame the character moves in the direction of the input          
                                                    //}

            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime; // fixedDeltaTime so the speed is constant with different framerates        

            isoRenderer.SetDirection(movement, isDashing);

            rbody.MovePosition(newPos);
        }
    }   
    private void DashFreeze()
    {
        isDashing = false;
    }
    private void DashCooldown()
    {
        dashRecover = true;
    }
}
