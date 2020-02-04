using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeState : EnemyBaseState
{
    private Drone drone;
    //constructor
    public EscapeState(Drone _drone) : base(_drone.gameObject)
    {
        drone = _drone;
    }

    //this operates like Update() function
    public override Type Tick()
    {
        transform.Translate(Vector3.down * Time.deltaTime * drone.aISettings.chaseSpeed);
        //active phase shift jump
        return null;
    }
}
