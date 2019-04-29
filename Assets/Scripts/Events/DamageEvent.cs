using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DamageEvent : GameEvent
{
    public int Damage { get; private set; }
    public Transform Target { get; private set; }
    public bool IsScreenSpace { get; private set; }
    
    public void ShowDamage(int damage, Transform target, bool screenSpace = false)
    {
        Damage = damage;
        Target = target;
        IsScreenSpace = screenSpace;
        Raise();
    }
}
