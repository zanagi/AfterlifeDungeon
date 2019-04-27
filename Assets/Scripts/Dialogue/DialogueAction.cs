using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueAction
{
    private static StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
    private static readonly string commentPrefix = "#", imagePrefix = "*";

    public static DialogueAction[] Parse(string txt)
    {
        // Split text to lines
        string[] lines = txt.Split(new[] { '\r', '\n' }, options);
        DialogueAction[] actionList = new DialogueAction[lines.Length];

        for(int i = 0; i < lines.Length; i++)
        {
            actionList[i] = TextToAction(lines[i]);
        }
        return actionList;
    }

    private static DialogueAction TextToAction(string text)
    {
        string trim = text.Trim();

        if (trim.Length == 0 || trim.StartsWith(commentPrefix))
        {
            return null;
        }
        else if (trim.StartsWith(imagePrefix))
        {
            return new DialogueActionSprite(trim.Substring(imagePrefix.Length));
        }
        return new DialogueActionText(trim);
    }

    public abstract IEnumerator PlayAction(DialogueManager manager);
}

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