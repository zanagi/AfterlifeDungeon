using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ChoiceStruct
{
    public string choice;
    public Dialogue dialogue;
}

[CreateAssetMenu(menuName = "Dialogue/Choice")]
public class DialogueChoice : ScriptableObject
{
    public ChoiceStruct[] choices;
}
