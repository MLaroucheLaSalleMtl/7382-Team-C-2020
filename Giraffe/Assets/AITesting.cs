using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITesting : MonoBehaviour
{
    private enum BossActionState
    {
        Moving,
        Idle,
        Following
    }
    private BossActionState CurrentState = BossActionState.Following;
    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case BossActionState.Moving:
                {
                    
                }break;
            case BossActionState.Idle:
                {
                    
                }
                break;
            case BossActionState.Following:
                {
                    Follow();
                }
                break;
        }
    }
    [SerializeField]private Vector3 distance = new Vector3();
    private void Follow()
    {
        distance = player.position - transform.position;
        transform.position = transform.position + (distance - new Vector3(3f, 0, 3f));
    }
}
