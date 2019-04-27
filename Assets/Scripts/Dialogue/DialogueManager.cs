using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueAnimMode
{
    None, Sprite, Text
}

public class DialogueManager : MonoBehaviour
{
    public DialogueEvent dialogueEvent;
    public Sprite[] sprites;

    public void PlayDialogue()
    {
        string dialogueText = dialogueEvent.DialogueAsset.text;
    }

    public IEnumerator AnimateSprite(string spriteNamew)
    {

        yield return null;
    }

    public IEnumerator AnimateText(string name, string text)
    {
        yield return null;
    }
}
