using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic wandering AI
public class WanderingAI : BaseAI
{
    public float wanderRadius = 3.0f, speed = 1.0f, maxWanderTime = 5.0f, pauseTime = 2.0f;

    private Vector3 startPos, previousPos, targetPos;
    private float wanderTime, currentPauseTime;
    private bool paused;

    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;
        SetTargetPos();
    }
    
    private void SetTargetPos()
    {
        targetPos = startPos + 
            new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius));
    }

    public override void HandleFixedUpdate(Player player)
    {
        // Check pause
        if(paused)
        {
            currentPauseTime += Time.fixedDeltaTime;
            rBody.velocity = Vector3.zero;

            if (currentPauseTime >= pauseTime)
            {
                wanderTime = currentPauseTime = 0;
                paused = false;
                SetTargetPos();
            }
            return;
        }

        // Check Wander time
        wanderTime += Time.fixedDeltaTime;

        if(wanderTime >= maxWanderTime)
        {
            paused = true;
            return;
        }

        // Check Movement
        Vector3 delta = targetPos - transform.position;
        float deltaLength = delta.magnitude;

        if(deltaLength <= speed)
        {
            paused = true;
            return;
        }
        rBody.velocity = delta / deltaLength * speed;
    }
}
