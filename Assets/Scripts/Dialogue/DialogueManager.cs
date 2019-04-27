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

    [Header("UI")]
    public Image mainImage;
    public Image textBoxImage, nameBoxImage;
    public Text mainText, nameText;
    public float textCharTime = 0.04f;
    private Canvas canvas;
    private bool inputNext;

    [Header("State Event")]
    public StateChangeEvent stateChangeEvent;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        inputNext = Input.GetMouseButtonDown(0);
    }

    public void SetVisible(bool visible)
    {
        canvas.enabled = visible;

        // Reset UI values when hiding canvas
        if(!visible)
        {
            mainText.text = nameText.text = string.Empty;
            mainImage.enabled = false;
        }
    }

    public void PlayDialogue()
    {
        if (stateChangeEvent.ChangeState(GameState.Event))
        {
            DialogueAction[] actions = DialogueAction.Parse(dialogueEvent.DialogueAsset.text);
            StartCoroutine(PlayDialogueActions(actions));
        }
    }

    private IEnumerator PlayDialogueActions(DialogueAction[] actions)
    {
        yield return AnimateUI(0, 1);

        for(int i = 0; i < actions.Length; i++)
        {
            if(actions[i] != null)
            {
                yield return actions[i].PlayAction(this);
            }
        }
        yield return AnimateUI(1, 0);
        stateChangeEvent.ChangeState(GameState.Idle);
    }

    private IEnumerator AnimateUI(float start, float end)
    {
        if(end >= 1)
            SetVisible(true);

        yield return null;

        if (end <= 0)
            SetVisible(false);
    }

    public IEnumerator AnimateSprite(string spriteName)
    {
        mainImage.enabled = true;
        mainImage.sprite = GetSprite(spriteName);
        yield return null;
    }

    private Sprite GetSprite(string spriteName)
    {
        for(int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == spriteName)
                return sprites[i];
        }
        return null;
    }

    public IEnumerator AnimateText(string name, string text)
    {
        nameText.text = name;
        mainText.text = string.Empty;

        for(int i = 0; i < text.Length; i++)
        {
            float currentCharTime = 0;
            while (currentCharTime < textCharTime && !inputNext)
            {
                currentCharTime += Time.deltaTime;
                yield return null;
            }
            if (inputNext)
            {
                mainText.text = text;
                yield return null;
                break;
            }

            mainText.text += text[i];
            yield return null;
        }

        while (!inputNext)
            yield return null;
        yield return null;
    }
}
