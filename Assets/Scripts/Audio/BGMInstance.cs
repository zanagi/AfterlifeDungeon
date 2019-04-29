using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMInstance : MonoBehaviour
{
    public AudioClip bgmClip;
    public float volume = 1.0f;
    public BGMEvent bgmEvent;

    // Start is called before the first frame update
    void Start()
    {
        bgmEvent.ChangeBGM(bgmClip, volume);
    }
}
