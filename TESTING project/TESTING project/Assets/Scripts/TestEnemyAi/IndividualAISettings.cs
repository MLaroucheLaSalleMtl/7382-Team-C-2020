using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//use this to control individual ship behaviour
public class IndividualAISettings : MonoBehaviour
{
    [SerializeField] private float bossID;
    [SerializeField] private float retreatValue;
    [SerializeField] private float maxAttackRange;
    [SerializeField] private float attackRange;
    public float arenaRange;
    public float wanderSpeed;
    public float chaseSpeed;
    public float turnSpeed;
    
    public float AttackSpeed()
    {
        return Random.Range(-1, 2);
    }
    public float frenzyLevel;
    public int detectionRange;
    public LayerMask opponentLayers;
    public LayerMask obstacleLayerMask;

    public float BossID { get => bossID; set => bossID = value; }
}