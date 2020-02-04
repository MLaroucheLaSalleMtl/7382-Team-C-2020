using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.beep;

public class WanderState : EnemyBaseState
{
    private Vector3? destination;
    private float stopDistance = 1.0f;
    private float turnSpeed = 1.0f;
    private Quaternion desiredRotation; //targetRotation;
    private Vector3 direction;
    private Quaternion startingAngle = Quaternion.AngleAxis(-60.0f, Vector3.up);
    private Quaternion stepAngle = Quaternion.AngleAxis(5.0f, Vector3.up);
    private Drone drone;

    

    //constructor
    public WanderState(Drone _drone) : base(_drone.gameObject)
    {
        drone = _drone;
    }

    //this operates like Update() function
    public override Type Tick()
    {
        //check for target
        Transform chaseTarget = CheckForTarget();
        if(chaseTarget != null)
        {
            float attackPattern = (UnityEngine.Random.Range(1, 5) * drone.aISettings.AggroValue);
            if(attackPattern <= 25)
            {
                return typeof(RangeAttackState);
            }
            else
            {
                drone.SetTarget(chaseTarget);
                return typeof(ChaseState);
            }
        }

        //if no target wnader aimlessly
        if (destination.HasValue == false || Vector3.Distance(transform.position, destination.Value) <= stopDistance)
        {
            FindRandomDestination();
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * drone.aISettings.turnSpeed);
        if(IsForwardBlocked())
        {
            FindRandomDestination();
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * drone.aISettings.wanderSpeed);
        }
        return null;
    }

    private Transform CheckForTarget()
    {
        if (Vector3.Distance(drone.Target.position, transform.position) <= drone.aISettings.ArenaRange)
        {
            return drone.Target.transform;
        }
        //RaycastHit2D hit2D;
        //var angle = transform.rotation * startingAngle;
        //direction = angle * Vector3.up;
        //var pos = transform.position;
        //for(int i = 0; i < 24; i++)
        //{
        //    hit2D = Physics2D.Raycast(pos, direction, drone.aISettings.detectionRange, drone.aISettings.opponentLayers);
        //    if(hit2D.collider != null)
        //    {
        //        //var opponent = hit2D.collider.GetComponent<Drone>();
        //        //return opponent.transform;
        //    }
        //    direction = stepAngle * direction;
        //}
        return null;
    }

    private bool IsForwardBlocked()
    {
        RaycastHit2D blockedHit = Physics2D.CircleCast(transform.position, 1.0f, transform.up, 4.0f, drone.aISettings.obstacleLayerMask);
        return blockedHit;
    }

    void FindRandomDestination()
    {
        Vector3 testPostion = (transform.position + (transform.up * 4.0f)) + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), UnityEngine.Random.Range(-4.5f, 4.5f), 0.0f);
        destination = new Vector3(testPostion.x, testPostion.y, 0.0f);
        direction = Vector3.Normalize(destination.Value - transform.position);
        Vector3 dir = transform.position - destination.Value;
        desiredRotation = Quaternion.LookRotation(dir, Vector3.forward);
        desiredRotation.z = desiredRotation.y;
        desiredRotation.x = 0.0f;
        desiredRotation.y = 0.0f;
    }
}