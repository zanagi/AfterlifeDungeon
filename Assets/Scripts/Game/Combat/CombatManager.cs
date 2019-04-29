using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CombatManager : MonoBehaviour
{
    public CombatEvent combatEvent;
    public StateChangeEvent stateEvent;
    public GameObject combatScreen;

    [Header("Start anim")]
    public float startAnimTime = 1.0f, startPause = 0.5f;
    public CanvasGroup startScreen;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponentInParent<GameManager>();
    }

    public void StartCombat()
    {
        if (stateEvent.ChangeState(GameState.Combat))
        {
            StartCoroutine(Combat());
        }
    }

    private IEnumerator Combat()
    {
        yield return AnimateStartScreen(0, 1);
        gameManager.player.gameObject.SetActive(false);
        combatScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(startPause);
        yield return AnimateStartScreen(1, 0);
    }

    private IEnumerator AnimateStartScreen(float a, float b)
    {
        if (a <= 0)
            startScreen.gameObject.SetActive(true);

        float time = 0f;
        while(time < startAnimTime)
        {
            time += Time.deltaTime;
            startScreen.alpha = Mathf.Lerp(a, b, time / startAnimTime);
            yield return null;
        }

        if (b <= 0)
            startScreen.gameObject.SetActive(false);
    }
}
