using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationPerSecond;
    public bool randomize;

    private void FixedUpdate()
    {
        if(randomize)
        {
            rotationPerSecond = 
                Quaternion.Euler(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * rotationPerSecond;
        }

        transform.Rotate(rotationPerSecond * Time.fixedDeltaTime);
    }
}
