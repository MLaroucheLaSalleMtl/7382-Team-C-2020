using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drone : MonoBehaviour
{
    public IndividualAISettings aISettings;
    private FireBossScript fireboss;
    
    //private NPCWeaponSystem myWeaponSystem;

    public Transform Target;// { get; private set; }

    public EnemyStateMachine EnemyStateMachine => GetComponent<EnemyStateMachine>();

    private void Awake()
    {
        InitialiseStateMachine();
        fireboss = FireBossScript.instance;
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
            { typeof(RetreatState), new RetreatState(this)}
        };
        GetComponent<EnemyStateMachine>().SetState(states);
    }

    public void SetTarget(Transform _target)
    {
        Target = _target;
    }
    [SerializeField] private GameObject fireball;
    private GameObject fuck;
    [SerializeField] private Vector2 direction = new Vector2();
    [SerializeField] private GameObject aoeMelee;

    public void FireLineTest()
    {
        
        switch (aISettings.BossID)
        {
            default:
                {
                    fireboss.Attack(); 
                }break;
                
        }
        
    }

    
    
}
