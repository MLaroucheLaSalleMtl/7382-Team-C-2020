using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    private IsometricPlayerMovementController isoMovement;
    
    public float h = 0;
    public float v = 0;
    public bool inDashing = false;
    bool attackLight = false;
    bool attackStrong = false;
    bool ranged = false;
    bool isPaused = false;
    bool chargedRanged = false;

    private bool dash = false;
    // Start is called before the first frame update
    void Start()
    {
        isoMovement = GetComponent<IsometricPlayerMovementController>();
        
    }

    private void FixedUpdate()
    {
        isoMovement.Move(h, v, dash);
        dash = false;
    }
    // Update is called once per frame
    void Update()
    {
            
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        h = move.x;
        v = move.y;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //Debug.Log("test");
            dash = true;
            //Debug.Log(dash);
        } 
        
    }

    public void OnAttackL(InputAction.CallbackContext context)
    {
        attackLight = context.performed;
        Debug.Log("Light Attack!");
    }

    public void OnAttackS(InputAction.CallbackContext context)
    {
        attackStrong = context.performed;
        Debug.Log("Strong Attack!");
    }

    public void OnRangedQuick(InputAction.CallbackContext context)
    {
        ranged = context.performed;
        Debug.Log("Ranged");
    }

    public void OnChargedRanged(InputAction.CallbackContext context)
    {
        chargedRanged = context.performed;
        Debug.Log("Charged Range");
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPaused = !isPaused;
            Debug.Log("Pause!");
        }
    }
}
