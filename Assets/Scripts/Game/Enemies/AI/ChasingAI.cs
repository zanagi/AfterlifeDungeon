using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic wandering AI
public class ChasingAI : WanderingAI
{
    public float range = 6.0f;

    public override void HandleFixedUpdate(Player player)
    {
        Vector3 delta = player.transform.position - transform.position;
        float deltaLength = delta.magnitude;
        if (deltaLength <= range)
        {
            rBody.velocity = delta / deltaLength * speed;
            return;
        }
        base.HandleFixedUpdate(player);
    }
}
