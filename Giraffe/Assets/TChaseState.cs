using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TChaseState : StateMachineBehaviour
{
    private GameObject test;
   [SerializeField] private float speed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        test = GameObject.FindGameObjectWithTag("Test");
    }
    private float xTest;
    private float zTest;
    private float x;
    private float z;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        xTest = test.transform.position.x;
        zTest = test.transform.position.z;
        x = animator.transform.position.x;
        z = animator.transform.position.z;

        animator.transform.position = Vector3.MoveTowards(animator.transform.position, test.transform.position, Time.deltaTime * 2f);
        if (Mathf.Sqrt(Mathf.Pow(xTest - x, 2) + Mathf.Pow(zTest - z, 2)) <= 10f) 
        {
            Debug.Log("test");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Follow", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
