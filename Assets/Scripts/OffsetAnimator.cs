using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetAnimator : MonoBehaviour
{
    public float animationSpeed = 0.5f;
    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * animationSpeed;
        _renderer.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
    }
}
