using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManagerIntro2 : ScenarioManager
{
    public DialogueEvent dialogueEvent;
    public float pause = 0.2f;

    [Header("1")]
    public Dialogue dialogue1;
    public CanvasGroup redScreen;
    public AudioSource hitSound;

    [Header("2")]
    public Dialogue dialogue2;
    
    [Header("Next")]
    public LoadEvent loadEvent;
    public string nextScene;


    private void Start()
    {
        redScreen.gameObject.SetActive(false);
        dialogueEvent.PlayDialogue(dialogue1);
    }

    public override void PlayScenario(Scenario scenario)
    {
        switch(scenario)
        {
            case Scenario.Intro2_1:
                StartCoroutine(PlayIntro2_1());
                break;
            case Scenario.Intro2_2:
                StartCoroutine(PlayIntro2_2());
                break;
        }
    }

    private IEnumerator PlayIntro2_1()
    {
        hitSound.Play();
        redScreen.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(pause);
        dialogueEvent.PlayDialogue(dialogue2);
    }

    private IEnumerator PlayIntro2_2()
    {
        yield return null;
        loadEvent.LoadScene(nextScene);
    }
}
