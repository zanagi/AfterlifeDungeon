using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

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
    public Sprite emptySprite;
    public float textBoxAlpha = 0.5f;
    public float animationTime = 0.2f, pauseTime = 0.2f, textCharTime = 0.04f;
    private Canvas canvas;
    private bool inputNext;

    [Header("Choice")]
    public Transform choiceTransform;
    public ChoiceButton choiceButtonPrefab;
    public GameEvent lockCursorEvent, unlockCursorEvent;
    public float choiceWaitTime = 0.2f;

    [Header("State Event")]
    public StateChangeEvent stateChangeEvent;

    [Header("Audio")]
    public GameEvent dialogueNextEvent;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        SetVisible(false);
    }

    private void Update()
    {
        inputNext = CrossPlatformInputManager.GetButtonDown(Static.mouseClickAxis) 
            || CrossPlatformInputManager.GetButtonDown(Static.submitAxis);
    }

    public void SetVisible(bool visible)
    {
        canvas.enabled = visible;


        if (!visible)
            EmptyUI();
    }

    private void EmptyUI()
    {
        mainText.text = nameText.text = string.Empty;
        mainImage.sprite = emptySprite;
        mainImage.enabled = false;
    }

    public void PlayDialogue()
    {
        if (stateChangeEvent.ChangeState(GameState.Event))
        {
            DialogueAction[] actions = DialogueAction.Parse(dialogueEvent.Dialogue.dialogue.text);
            StartCoroutine(PlayDialogueActions(actions));
        }
    }

    private IEnumerator PlayDialogueActions(DialogueAction[] actions)
    {
        // Unlock cursor for choice
        unlockCursorEvent.Raise();

        yield return AnimateUI(0, 1);
        yield return PlayDialogueActionsOnly(actions);
        yield return AnimateUI(1, 0);

        // Lock cursor again
        lockCursorEvent.Raise();

        stateChangeEvent.ChangeState(GameState.Idle);

        // Let scenario manager handle possible end scenarios
        if (ScenarioManager.Instance)
        {
            Scenario endScenario = dialogueEvent.Dialogue.endScenario;
            ScenarioManager.Instance.PlayScenario(endScenario);
        }
    }

    private IEnumerator PlayDialogueActionsOnly(DialogueAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i] != null)
            {
                yield return actions[i].PlayAction(this);
            }
        }
        if (dialogueEvent.Dialogue.choiceObject)
        {
            yield return new WaitForSecondsRealtime(choiceWaitTime);
            yield return AnimateChoices(dialogueEvent.Dialogue.choiceObject);
        }
        yield return null;
    }

    private IEnumerator AnimateUI(float start, float end)
    {
        if (end >= 1)
            SetVisible(true);

        // Animate the values of UI elements
        float time = 0.0f;
        while(time <= animationTime)
        {
            time += Time.deltaTime;

            float target = Mathf.Lerp(start, end, time / animationTime);
            textBoxImage.SetAlpha(target * textBoxAlpha);
            textBoxImage.transform.localScale = new Vector3(target, target);
            nameBoxImage.SetAlpha(target);
            // nameBoxImage.transform.localScale = new Vector3(target, target);

            if (mainImage.enabled)
                mainImage.SetAlpha(target);
            yield return null;
        }

        if (end <= 0)
            SetVisible(false);
        yield return new WaitForSecondsRealtime(pauseTime);
    }

    public IEnumerator AnimateSprite(string spriteName)
    {
        if (mainImage.enabled && mainImage.sprite && mainImage.sprite != emptySprite)
        {
            yield return AnimateSpriteFade(1, 0);
        } else
        {
            mainImage.enabled = true;
        }
        mainImage.sprite = GetSprite(spriteName);
        yield return AnimateSpriteFade(0, 1);
        yield return null;
    }

    private IEnumerator AnimateSpriteFade(float a, float b)
    {
        float time = 0.0f;
        while (time <= animationTime)
        {
            time += Time.deltaTime;

            float alpha = Mathf.Lerp(a, b, time / animationTime);
            mainImage.GetComponent<CanvasGroup>().alpha = alpha;
            yield return null;
        }
        yield return null;
    }

    private Sprite GetSprite(string spriteName)
    {
        for(int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == spriteName)
                return sprites[i];
        }
        return emptySprite;
    }

    public IEnumerator AnimateText(string name, string text)
    {
        nameText.text = name;
        nameBoxImage.gameObject.SetActive(name != null && name.Length > 0);

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
        dialogueNextEvent.Raise();
        yield return null;
    }

    public IEnumerator AnimateChoices(DialogueChoice choiceObject)
    {
        ChoiceStruct[] choices = choiceObject.choices;
        int choiceCount = choices.Length;
        ChoiceButton[] buttons = new ChoiceButton[choiceCount];
        int index = -1;

        for(int i = 0; i < choiceCount; i++)
        {
            ChoiceButton button = choiceButtonPrefab.Spawn(choiceTransform);
            button.text.text = choices[i].choice;
            buttons[i] = button;
        }

        while(index < 0)
        {
            for(int i = 0; i < choiceCount; i++)
            {
                if(buttons[i].clicked)
                {
                    index = i;
                    break;
                }
            }
            yield return null;
        }

        for (int i = 0; i < choiceCount; i++)
            buttons[i].Recycle();

        // Play following dialogue
        // TODO: Somehow enable another choice in dialogue?
        Dialogue nextDialogue = choices[index].dialogue;
        dialogueEvent.SetDialogue(nextDialogue);
        yield return PlayDialogueActionsOnly(DialogueAction.Parse(nextDialogue.dialogue.text));
    }
}
