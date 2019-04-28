using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimator : MonoBehaviour
{
    public float minScale = 1.0f, maxScale = 2.0f, speed = 5.0f;
    public StateChangeEvent state;
    private float scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = Random.Range(minScale, maxScale);
        transform.localScale = Vector3.one * scale;
    }

    // Update is called once per frame
    void Update()
    {
        if (state.CurrentState != GameState.Idle)
            return;

        scale += speed * Time.deltaTime;
        if(scale > maxScale)
        {
            scale = maxScale;
            speed *= -1;
        } else if(scale < minScale) {
            scale = minScale;
            speed *= -1;
        }
        transform.localScale = Vector3.one * scale;
    }
}
