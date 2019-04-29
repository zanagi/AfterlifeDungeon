using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public float animTime = 0.2f, pauseTime = 0.5f;
    private Text text;

    public void Play(int damage, Camera combatCamera, Transform targetTransform, bool screenSpace = false)
    {
        if (!text)
            text = GetComponent<Text>();
        text.text = string.Empty;
        StartCoroutine(_Play(damage, combatCamera, targetTransform, screenSpace));
    }

    public IEnumerator _Play(int damage, Camera combatCamera, Transform targetTransform, bool screenSpace)
    {
        text.text = damage.ToString();

        Vector3 targetScale = Vector3.one;
        Vector3 startScale = Vector3.zero;
        transform.localScale = startScale;

        float time = 0f;
        while (time < animTime)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / animTime);
            transform.position = screenSpace ? targetTransform.position :
                combatCamera.WorldToScreenPoint(targetTransform.position);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(pauseTime);
        time = 0f;
        while (time < animTime)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale, startScale, time / animTime);
            yield return null;
        }
        gameObject.Recycle();
    }
}
