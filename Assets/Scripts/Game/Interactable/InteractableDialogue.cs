using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueEvent dialogueEvent;

    public void PlayDialogue()
    {
        dialogueEvent.PlayDialogue(dialogue);
    }
}
