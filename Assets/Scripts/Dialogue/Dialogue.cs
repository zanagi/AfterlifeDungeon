using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{
    public TextAsset dialogue;
    public DialogueChoice choiceObject;
    public Scenario endScenario;
}
