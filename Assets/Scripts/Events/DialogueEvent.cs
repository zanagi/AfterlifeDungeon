using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Event for loading a new scene
[CreateAssetMenu(menuName = "Game Event/Dialogue Event")]
public class DialogueEvent : GameEvent
{
    public TextAsset DialogueAsset { get; private set; }

    public void PlayDialogue(TextAsset asset)
    {
        DialogueAsset = asset;
        Raise();
    }
}
