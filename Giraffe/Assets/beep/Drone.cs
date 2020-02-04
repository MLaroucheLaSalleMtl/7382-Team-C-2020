using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.beep;

public class Drone : MonoBehaviour
{
    public IndividualAISettings aISettings;
    [SerializeField]
    //private NPCWeaponSystem myWeaponSystem;

    public Transform Target;// { get; set; }

    public EnemyStateMachine EnemyStateMachine => GetComponent<EnemyStateMachine>();

    private void Awake()
    {
        InitialiseStateMachine();
        //myWeaponSystem = GetComponentInChildren<NPCWeaponSystem>();
    }

    private void InitialiseStateMachine()
    {
        //look at adding states like RetreatState, FrenzyState
        var states = new Dictionary<Type, EnemyBaseState>()
        {
            { typeof(WanderState), new WanderState(this)},
            { typeof(ChaseState), new ChaseState(this)},
            { typeof(AttackState), new AttackState(this)},
            { typeof(EscapeState), new EscapeState(this)},
            { typeof(RetreatState), new RetreatState(this)},
            { typeof(RangeAttackState), new RangeAttackState(this) }
        };
        GetComponent<EnemyStateMachine>().SetState(states);
    }

    public void SetTarget(Transform _target)
    {
        Target = _target;
    }

    public void FireWeapon(bool _fireState)
    {
        //myWeaponSystem.FireWeaponSlave(_fireState);
    }
    [SerializeField] private GameObject test;
    public void Attack()
    {
        switch (aISettings.BossID)
        {
            case 0:
                {
                    Boss0Attacks.Attack(0);
                }break;
        }
        test = Instantiate(test, transform.position, transform.rotation);
        
        test.GetComponent<Rigidbody>().AddForce(Target.position * 200f);
    }
}
