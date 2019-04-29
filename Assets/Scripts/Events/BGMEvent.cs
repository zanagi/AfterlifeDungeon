using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BGM Event")]
public class BGMEvent : GameEvent
{
    public AudioClip AudioClip { get; private set; }
    public float Volume { get; private set; }

    public void ChangeBGM(AudioClip clip, float volume = 1.0f)
    {
        AudioClip = clip;
        Volume = volume;
        Raise();
    }
}
