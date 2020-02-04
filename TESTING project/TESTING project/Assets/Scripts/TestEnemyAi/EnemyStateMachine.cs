using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EnemyStateMachine : MonoBehaviour
{
    private Dictionary<Type, EnemyBaseState> availableStates;

    public EnemyBaseState CurrentState { get; private set; }
    public event Action<EnemyBaseState> OnStateChanged;

    public void SetState(Dictionary<Type, EnemyBaseState> states)
    {
        availableStates = states;
    }

    // Update is called once per frame
    private void Update ()
    {
		if(CurrentState == null)
        {
            CurrentState = availableStates.Values.First();
        }

        var nextState = CurrentState?.Tick();

        if(nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = availableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
}
