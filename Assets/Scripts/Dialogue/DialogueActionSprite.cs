using UnityEngine;
using System.Collections;

public class DialogueActionSprite : DialogueAction
{
    private string spriteName = string.Empty;

    public DialogueActionSprite(string name)
    {
        spriteName = name;
    }

    public override IEnumerator PlayAction(DialogueManager manager)
    {
        yield return manager.AnimateSprite(spriteName);
    }
}