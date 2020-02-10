using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : EnemyBaseState
{

    private Drone drone;
    //constructor
    public RetreatState(Drone _drone) : base(_drone.gameObject)
    {
        drone = _drone;
    }

    //this operates like Update() function
    public override Type Tick()
    {
        transform.Translate(Vector3.down * Time.deltaTime * drone.aISettings.chaseSpeed);
        if(Vector3.Distance(transform.position, drone.Target.position) >= drone.aISettings.AttackRange)
        {
            //drone.FireWeapon(false);
            return typeof(ChaseState);
        }
        return null;
    }
}
