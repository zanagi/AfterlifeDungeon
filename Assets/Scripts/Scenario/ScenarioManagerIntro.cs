using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManagerIntro : ScenarioManager
{
    public DialogueEvent dialogueEvent;
    public Dialogue dialogueA;
    public float pause = 0.2f;

    [Header("A")]
    public CanvasGroup blackScreen;
    public float blackScreenTime = 3.0f;

    [Header("B")]
    public Dialogue dialogueB;
    public Transform other, target;
    public float bTime = 3.0f;

    [Header("C")]
    public Dialogue dialogueC;
    public Transform cOther, cTarget;
    public float cTime = 1.0f;

    [Header("Next")]
    public LoadEvent loadEvent;
    public string nextScene;


    private void Start()
    {
        blackScreen.gameObject.SetActive(true);
        dialogueEvent.PlayDialogue(dialogueA);
    }

    public override void PlayScenario(Scenario scenario)
    {
        switch(scenario)
        {
            case Scenario.IntroA:
                StartCoroutine(PlayIntroA());
                break;
            case Scenario.IntroB:
                StartCoroutine(PlayIntroB());
                break;
            case Scenario.IntroC:
                StartCoroutine(PlayIntroC());
                break;
        }
    }

    private IEnumerator PlayIntroA()
    {
        float time = 0f;

        while(time <= blackScreenTime)
        {
            time += Time.deltaTime;
            blackScreen.alpha = 1.0f - time / blackScreenTime;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(pause);
        dialogueEvent.PlayDialogue(dialogueB);
    }

    private IEnumerator PlayIntroB()
    {
        float time = 0f;
        Vector3 startPos = other.position;
        Vector3 endPos = target.position;

        while (time <= bTime)
        {
            time += Time.deltaTime;
            other.position = Vector3.Lerp(startPos, endPos, time / bTime);
            yield return null;
        }
        other.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(pause);
        dialogueEvent.PlayDialogue(dialogueC);
    }

    private IEnumerator PlayIntroC()
    {
        float time = 0f;
        Vector3 startPos = cOther.position;
        Vector3 endPos = cTarget.position;

        while (time <= cTime)
        {
            time += Time.deltaTime;
            cOther.position = Vector3.Lerp(startPos, endPos, time / cTime);
            blackScreen.alpha = time / cTime;
            yield return null;
        }
        loadEvent.LoadScene(nextScene);
    }
}
