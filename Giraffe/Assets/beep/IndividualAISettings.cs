using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//use this to control individual ship behaviour
public class IndividualAISettings : MonoBehaviour
{
    [SerializeField] private int bossID;
    [SerializeField] private int stages; 
    [SerializeField] private int attackarray;
    [SerializeField] private float arenaRange;
    [SerializeField] private float aggroValue;
    public float wanderSpeed;
    public float chaseSpeed;
    public float turnSpeed;
    public float attackRange;
    public float AttackSpeed()
    {
        return Random.Range(-1, 2);
    }
    public float frenzyLevel;
    public int detectionRange;
    public LayerMask opponentLayers;
    public LayerMask obstacleLayerMask;

    public float ArenaRange { get => arenaRange; set => arenaRange = value; }
    public int BossID { get => bossID; set => bossID = value; }
    public float AggroValue { get => aggroValue; set => aggroValue = value; }
}