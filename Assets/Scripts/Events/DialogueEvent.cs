using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Event for loading a new scene
[CreateAssetMenu(menuName = "Game Event/Dialogue Event")]
public class DialogueEvent : GameEvent
{
    public Dialogue Dialogue { get; private set; }

    public void PlayDialogue(Dialogue dialogue)
    {
        Dialogue = dialogue;
        Raise();
    }

    public void SetDialogue(Dialogue dialogue)
    {
        Dialogue = dialogue;
    }
}
