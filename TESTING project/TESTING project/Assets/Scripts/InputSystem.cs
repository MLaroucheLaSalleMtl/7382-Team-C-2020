using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    private IsometricPlayerMovementController isoMovement;
    
    float h = 0;
    float v = 0;
    bool dash = false;
    bool attackLight = false;
    bool attackStrong = false;
    bool ranged = false;
    bool isPaused = false;
    bool chargedRanged = false;


    // Start is called before the first frame update
    void Start()
    {
        isoMovement = GetComponent<IsometricPlayerMovementController>();
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
        dash = context.performed;
        Debug.Log("DASH!");
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
