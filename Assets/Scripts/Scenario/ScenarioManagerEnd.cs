using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManagerEnd : ScenarioManager
{
    public DialogueEvent dialogueEvent;
    public Dialogue dialogue1;
    public LoadEvent loadEvent;
    public string nextScene;

    private void Start()
    {
        dialogueEvent.PlayDialogue(dialogue1);
        if (GameManager.Instance)
            Destroy(GameManager.Instance.gameObject);
    }
    
    public override void PlayScenario(Scenario scenario)
    {
        switch (scenario)
        {
            case Scenario.End:
                loadEvent.LoadScene(nextScene);
                break;
        }
    }
}
