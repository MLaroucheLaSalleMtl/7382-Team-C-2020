using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyBaseState
{
    private Drone drone;
    //constructor
    public ChaseState(Drone _drone) : base(_drone.gameObject)
    {
        drone = _drone;
    }

    //this operates like Update() function
    public override Type Tick()
    {
        //what to do if there's no target
        if (drone.Target == null)
        {
            return typeof(WanderState);
        }
        if (Input.GetKey(KeyCode.L))
        {
            
            drone.FireLineTest();
        }
        
        //what to do in this state
        Vector3 dir = transform.position - drone.Target.position;
        Quaternion targetDirection = Quaternion.LookRotation(dir, Vector3.forward);
        targetDirection.z = targetDirection.y;
        targetDirection.x = 0.0f;
        targetDirection.y = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, Time.deltaTime * (drone.aISettings.turnSpeed * 0.5f));
        transform.Translate(Vector3.up * Time.deltaTime * drone.aISettings.chaseSpeed);
        //what to do if within attack range
        float distanceToTarget = Vector3.Distance(transform.position, drone.Target.transform.position);
        if(distanceToTarget <= drone.aISettings.AttackRange)
        {

            return typeof(AttackState);
        }

        return null;
    }
}
