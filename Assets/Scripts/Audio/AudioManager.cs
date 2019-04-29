using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgm;
    public BGMEvent bgmEvent;
    public float swapTime = 0.5f;
    private AudioClip backupClip;

    private void Start()
    {
        bgm.volume = 0;
    }

    public void ChangeBGM()
    {
        if (bgm.clip == bgmEvent.AudioClip)
            return;

        StopAllCoroutines();
        StartCoroutine(_ChangeBGM(bgmEvent.AudioClip));
    }

    public void RollbackBGM()
    {
        StopAllCoroutines();
        StartCoroutine(_RollbackBGM());
    }

    private IEnumerator _ChangeBGM(AudioClip clip, bool backup = true)
    {
        if (bgm.clip)
        {
            if(backup)
                backupClip = bgm.clip;
            yield return _ChangeBGMVolume(0);
        }
        bgm.clip = clip;
        yield return _ChangeBGMVolume(bgmEvent.Volume);
    }

    private IEnumerator _ChangeBGMVolume(float volume)
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
        {
            bgm.Stop();
        }
        else if (!bgm.isPlaying)
        {
            bgm.Play();
        }
    }

    private IEnumerator _RollbackBGM()
    {
        if(backupClip)
        {
            yield return _ChangeBGM(backupClip, false);
            backupClip = null;
        } else
        {
            yield return _ChangeBGMVolume(0);
        }
    }
}
