using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public float animTime = 0.2f, pauseTime = 0.5f;

    public IEnumerator _Play(int damage)
    {
        GetComponent<Text>().text = damage.ToString();

        Vector3 targetScale = Vector3.one;
        Vector3 startScale = Vector3.zero;
        transform.localScale = startScale;

        float time = 0f;
        while (time < animTime)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / animTime);
            yield return null;
        }
        time = 0f;
        while (time < animTime)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale, startScale, time / animTime);
            yield return null;
        }
    }
}
