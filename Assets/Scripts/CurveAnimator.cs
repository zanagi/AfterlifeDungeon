using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveAnimator : MonoBehaviour
{
    public float animationLength = 0.5f, animationStrength = 1;
    public AnimationCurve xCurve, yCurve;
    private float currentTime;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= animationLength)
            currentTime -= animationLength;

        float t = currentTime / animationLength;
        transform.localPosition = startPos + new Vector3(xCurve.Evaluate(t), yCurve.Evaluate(t)) * animationStrength;
    }
}
