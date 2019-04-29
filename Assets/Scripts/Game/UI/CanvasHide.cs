using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHide : MonoBehaviour
{
    public StateChangeEvent state;
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();

        if (!canvas)
            enabled = false;
    }
    
    void Update()
    {
        canvas.enabled = (state.CurrentState == GameState.Idle);
    }
}
