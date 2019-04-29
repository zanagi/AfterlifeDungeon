using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public float animTime = 0.2f;
    public IEnumerator _Play()
    {
        Vector3 targetScale = Vector3.one;
        Vector3 startScale = Vector3.zero;
        transform.localScale = startScale;

        float time = 0f;
        while(time < animTime)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / animTime);
            yield return null;
        }
        transform.localScale = startScale;
        
    }

    public IEnumerator _Play(Camera combatCamera, Transform targetTransform)
    {
        Vector3 targetScale = Vector3.one;
        Vector3 startScale = Vector3.zero;
        transform.localScale = startScale;

        float time = 0f;
        while (time < animTime)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / animTime);
            transform.position = combatCamera.WorldToScreenPoint(targetTransform.position);
            yield return null;
        }
        transform.localScale = startScale;
    }
}
