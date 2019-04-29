using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Image iconImage;
    [HideInInspector]
    public Skill skill;
    private Button button;
    private PartyMemberPanel partyPanel;
    private Outline outline;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => SelectButton());
        button.interactable = false;
        partyPanel = GetComponentInParent<PartyMemberPanel>();
        outline = GetComponent<Outline>();
    }

    public void CancelSelect()
    {
        outline.enabled = false;
    }

    public void SelectButton()
    {
        outline.enabled = true;
        partyPanel.SelectSkillButton(this);
    }

    public void SetActive(bool active)
    {
        button.interactable = active;
    }

    public void SetSkill(Skill skill)
    {
        if(skill)
        {
            button.interactable = true;
            iconImage.enabled = true;
            iconImage.sprite = skill.sprite;
        } else
        {
            button.interactable = false;
            iconImage.enabled = false;
            iconImage.sprite = null;
        }
        this.skill = skill;
    }
}
