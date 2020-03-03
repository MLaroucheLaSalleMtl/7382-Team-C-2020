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
    [SerializeField]private bool isDashing = false;
    [SerializeField]private bool dashRecover = true;
    private float horizontalInput;
    private float verticalInput;
    private Vector2 inputVector = new Vector2();
    private Vector2 currentPos = new Vector2();
    [SerializeField]private static float s = 0.5f;
    private static float cornerS = s * Mathf.Sin(45f);
    private static readonly Vector2[] quadrant = { new Vector2(0, s), new Vector2(-cornerS, cornerS), new Vector2(-s, 0), new Vector2(-cornerS, -cornerS), new Vector2(0, -s), new Vector2(cornerS, -cornerS), new Vector2(s, 0), new Vector2(cornerS, cornerS) };
    private int direction;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyLayer;
    private GameManager code;
    [SerializeField] private Transform middle;
    private AudioSource audio;
    // Update is called once per frame
    private void FixedUpdate()
    {
        //Move();
        //Dash();
    }
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        code = GameManager.instance;
    }
    private void DashFreeze()
    {
        isDashing = false;
    }
    private void DashCooldown()
    {
        dashRecover = true;
    }


    public void Move(Vector2 move, bool dash, bool attack)
    {
        //Debug.Log(dash);
        if (dash && !isDashing && dashRecover)
        {
            isDashing = true;
            dashRecover = false;
            Invoke("DashCooldown", dashCooldown);
            Invoke("DashFreeze", dashLength);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            movement = dashSpeed * inputVector;

        }
        else if (!isDashing)
        {
            inputVector = move;
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            movement = inputVector * movementSpeed;
        }
        currentPos = rbody.position;


        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;


        rbody.MovePosition(newPos);
        isoRenderer.SetDirection(movement, isDashing, attack);
    }
    
    public void Attack(Vector2 move)
    {
        
        direction = isoRenderer.lastDirection;
        //new Vector2(transform.position.x + move.x, transform.position.y + move.y);

        Collider2D[] hit = Physics2D.OverlapCircleAll(currentPos + new Vector2(0.1f,0.1f) + quadrant[direction], attackRadius, enemyLayer);

        if(hit.Length == 0) { audio.Play(); }
        foreach (Collider2D enemy in hit)
        {
            if (enemy.gameObject.GetComponent<BridgeBossAi>())
            {
                enemy.gameObject.GetComponent<BridgeBossAi>().GetHit(code.MeleeAttack);
            }
            else if(enemy.gameObject.GetComponent<ChaosBossAi>())
            {
                enemy.gameObject.GetComponent<ChaosBossAi>().GetHit(code.MeleeAttack);
                
            }
            
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(currentPos + new Vector2(0, 0.2f) + quadrant[direction], attackRadius);
    }
    //public void Dash()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && !isDashing && dashRecover)
    //    {
    //        isDashing = true;
    //        dashRecover = false;
    //        Invoke("DashFreeze", dashLength);
    //        Invoke("DashCooldown", dashCooldown);
    //        inputVector = Vector2.ClampMagnitude(inputVector, 1); // limit the magnitude thus preventing diagonal movement being faster than cardinal movement 
    //        movement = dashSpeed * inputVector;
    //    }
    //    else if (!isDashing)
    //    {
    //        horizontalInput = Input.GetAxis("Horizontal"); //get the input
    //        verticalInput = Input.GetAxis("Vertical");
    //        inputVector = new Vector2(horizontalInput, verticalInput); // store the input in a new vector 2 | the teacher said he doesnt want this , we have to use the new method
    //        inputVector = Vector2.ClampMagnitude(inputVector, 1); // limit the magnitude thus preventing diagonal movement being faster than cardinal movement 
    //        movement = inputVector * movementSpeed; // this makes it so for that frame the character moves in the direction of the input
    //    }

    //}
}
