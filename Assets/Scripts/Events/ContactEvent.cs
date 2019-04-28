using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Event for loading a new scene
[CreateAssetMenu(menuName = "Game Event/Interact/Contact Event")]
public class ContactEvent : GameEvent
{
    public string InteractText { get; private set; }

    public void OnContact(string text)
    {
        InteractText = text;
        Raise();
    }
}
