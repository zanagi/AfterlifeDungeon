using UnityEngine;
using System.Collections;

public class DialogueActionText : DialogueAction
{
    private string text, name;
    private bool textSet;
    private const char separator = ';', lineBreak = '^';

    public DialogueActionText(string text, string name)
    {
        this.text = text;
        this.name = name;
    }

    public DialogueActionText(string line)
    {
        var split = line.Split(separator);

        if (split.Length == 1)
            text = split[0].Trim();
        else if (split.Length > 1)
        {
            name = split[0].Trim();
            text = split[1].Trim().Replace(lineBreak, '\n');
        }
    }

    public override IEnumerator PlayAction(DialogueManager manager)
    {
        yield return manager.AnimateText(name, text);
    }
}
