using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgm;
    public BGMEvent bgmEvent;
    public float swapTime = 0.5f;

    private void Start()
    {
        bgm.volume = 0;
    }

    public void ChangeBGM()
    {
        if (bgm.clip == bgmEvent.AudioClip)
            return;

        StopAllCoroutines();
        StartCoroutine(SwapBGM());
    }

    private IEnumerator SwapBGM()
    {
        if(bgm.clip)
            yield return SetBGMVolume(0);
        bgm.clip = bgmEvent.AudioClip;
        yield return SetBGMVolume(bgmEvent.Volume);
    }

    private IEnumerator SetBGMVolume(float volume)
    {
        float time = 0f;
        float startVolume = bgm.volume;

        while(time < swapTime)
        {
            time += Time.deltaTime;
            bgm.volume = Mathf.Lerp(startVolume, volume, time / swapTime);
            yield return null;
        }

        if (volume <= 0)
            bgm.Stop();
        else
            bgm.Play();
    }
}
