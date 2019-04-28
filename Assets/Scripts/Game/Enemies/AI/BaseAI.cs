using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic wandering AI
public class BaseAI : MonoBehaviour
{
    protected Rigidbody rBody;

    protected virtual void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public virtual void Stop()
    {
        Debug.Log("Stop: " + name);
        rBody.velocity = Vector3.zero;
    }

    public virtual void HandleUpdate(Player player) { }
    public virtual void HandleFixedUpdate(Player player) { }
}
