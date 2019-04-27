using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public LoadEvent loadEvent;
    public DialogueEvent dialogueEvent;
    public TextAsset dialogue;
    public string sceneName = "Menu";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            loadEvent.LoadScene(sceneName);
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("Play test dialogue.");
            dialogueEvent.PlayDialogue(dialogue);
        }
    }
}
