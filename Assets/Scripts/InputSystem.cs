using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    private IsometricPlayerMovementController isoMovement;
    private GameManager code;
    private Vector2 move = new Vector2();
    public float h = 0;
    public float v = 0;
    public bool isDashing = false;
    bool attackLight = false;
    bool attackStrong = false;
    bool ranged = false;
    bool chargedRanged = false;

    public PauseManager pauseManager;
    private bool isPaused = true;

    private bool dash = false;
    [SerializeField]private bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        isoMovement = GetComponent<IsometricPlayerMovementController>();
        code = GameManager.instance;
    }

    private void FixedUpdate()
    {
        isoMovement.Move(move, dash, attack);
        
        dash = false;
    }
    private IEnumerator Test()
    {
        yield return new WaitForSecondsRealtime(0.12f);
        attack = false;
    }
    // Update is called once per frame
    void Update()
    {
            
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        //h = move.x;
        //v = move.y;
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
        if (context.started && !attack)
        {
            if(code.currentStamina >= 0)
            {
                
                attack = true;
                isoMovement.Attack(move);
                code.Attack();

            }
            
        }
        
        StartCoroutine(Test());
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
        isPaused = context.performed;
    }
}
