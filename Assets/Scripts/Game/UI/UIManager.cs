using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Interact/Contact")]
    public Text interactText;
    public float animationTime = 1.0f;
    public ContactEvent contactEvent;

    [Header("Life Bar")]
    public Lifebar lifebar;
    private Player player;

    private void Start()
    {
        interactText.SetAlpha(0);
        player = GetComponentInParent<GameManager>().player;

        lifebar.SetStats(player.stats);
    }

    public void SetInteractText()
    {
        string text = contactEvent.InteractText;
        // Animate text
        StopAllCoroutines();
        
        if(text.Length <= 0)
        {
            StartCoroutine(AnimateInteractText(0f));
        } else
        {
            interactText.text = text;
            StartCoroutine(AnimateInteractText(1f));
        }
    }

    private IEnumerator AnimateInteractText(float targetAlpha)
    {
        if (targetAlpha >= 0)
            interactText.enabled = true;
        
        float time = 0.0f;
        float currentAlpha = interactText.color.a;

        while(time <= animationTime)
        {
            time += Time.deltaTime;
            interactText.SetAlpha(Mathf.Lerp(currentAlpha, targetAlpha, time / animationTime));
            yield return null;
        }

        if (targetAlpha <= 0)
            interactText.enabled = false;
    }
}
