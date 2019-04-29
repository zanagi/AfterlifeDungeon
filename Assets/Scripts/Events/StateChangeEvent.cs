using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event/State Change Event")]
public class StateChangeEvent : GameEvent
{
    public GameState CurrentState { get; private set; }

    public bool ChangeState(GameState newState)
    {
        // Ignore same states and transitions to non-idle states (except combat to event above)
        if (CurrentState == newState || (CurrentState != GameState.Idle && newState != GameState.Idle))
            return false;
        CurrentState = newState;
        Raise();
        return true;
    }

    
}
