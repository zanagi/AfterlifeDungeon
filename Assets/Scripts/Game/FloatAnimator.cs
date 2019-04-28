using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAnimator : MonoBehaviour
{
    public float speed, power;
    public AnimationCurve curve;
    public StateChangeEvent state;
    private float height, time;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localPosition.y;
        time = Random.Range(0, 1.0f);
        speed = Random.Range(speed * 0.8f, speed * 1.25f);
    }
    
    void Update()
    {
        if (state.CurrentState != GameState.Idle)
            return;

        time += Time.deltaTime * speed;
        if (time >= 1.0f)
            time -= 1.0f;

        Vector3 localPos = transform.localPosition;
        localPos.y = height + curve.Evaluate(time) * power;
        transform.localPosition = localPos;
    }
}
