using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    public string nextLevel;
    public LoadEvent loadEvent;

    [Header("Dialogue")]
    public Dialogue blockDialogue;
    public DialogueEvent dialogueEvent;

    public void Advance()
    {
        if(blockDialogue)
        {
            dialogueEvent.PlayDialogue(blockDialogue);
            return;
        }

        loadEvent.LoadScene(nextLevel);
    }
}
