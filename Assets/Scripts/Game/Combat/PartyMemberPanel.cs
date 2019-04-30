using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberPanel : MonoBehaviour
{
    public Image memberImage;
    public SkillButton[] skillButtons;
    public Lifebar lifebar;

    [HideInInspector]
    public Stats userStats;
    [HideInInspector]
    public SkillButton selectedSkillButton;
    public SkillSelectEvent selectEvent;


    private CanvasGroup canvasGroup;
    private CombatManager manager;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        manager = GetComponentInParent<CombatManager>();
    }

    public bool UserAlive()
    {
        bool isAlive = userStats != null && userStats.hp > 0;
        return isAlive;
    }

    public void SetStats(Stats stats)
    {
        userStats = stats;
        UpdateUI();
    }

    public void SetActive(bool active)
    {
        canvasGroup.alpha = active ? 1f : 0.5f;

        for (int i = 0; i < skillButtons.Length; i++)
            skillButtons[i].SetActive(active);

        if (skillButtons.Length > 0)
            skillButtons[0].SelectButton();
    }

    public void SelectSkillButton(SkillButton skillButton)
    {
        selectedSkillButton = skillButton;
        selectEvent.SelectSkill(skillButton.skill, userStats, skillButton);
    }

    public void UpdateUI()
    {
        if(userStats == null)
        {
            memberImage.sprite = null;
            memberImage.color = new Color(0, 0, 0, 0);
            lifebar.gameObject.SetActive(false);
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].SetSkill(null);
            }
        } else
        {
            memberImage.sprite = userStats.sprite;
            memberImage.color = Color.white;

            lifebar.gameObject.SetActive(true);
            lifebar.SetStats(userStats);
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].SetSkill(i < userStats.skills.Count ? userStats.skills[i] : null);
            }
        }
    }
}
