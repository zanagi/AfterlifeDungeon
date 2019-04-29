using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DamageEvent : GameEvent
{
    public int Damage { get; private set; }
    public GameObject Target { get; private set; }
    
    public void ShowDamage(int damage, GameObject target)
    {
        Damage = damage;
        Target = target;
        Raise();
    }
}
