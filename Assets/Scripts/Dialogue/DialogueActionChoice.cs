using UnityEngine;
using System.Collections;

public class DialogueActionChoice : DialogueAction
{
    private string[] choices; 

    public DialogueActionChoice(string[] choices)
    {
        this.choices = choices;
    }

    public override IEnumerator PlayAction(DialogueManager manager)
    {
        yield return null;
    }
}