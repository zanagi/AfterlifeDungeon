using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueAction
{
    private static StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
    private static readonly string commentPrefix = "#", imagePrefix = "*", choicePrefix = "/";

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
        } else if(trim.StartsWith(choicePrefix))
        {
            return new DialogueActionChoice(trim.Split(new[] { choicePrefix }, options));
        }
        return new DialogueActionText(trim);
    }

    public abstract IEnumerator PlayAction(DialogueManager manager);
}