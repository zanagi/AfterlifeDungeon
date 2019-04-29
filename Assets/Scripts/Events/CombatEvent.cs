using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event/Combat/Combatx Event")]
public class CombatEvent : GameEvent
{
    public void StartCombat()
    {
        Raise();
    }
}
