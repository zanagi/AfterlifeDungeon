using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillSelectEvent : GameEvent
{
    public Skill Skill { get; private set; }
    public Stats UserStats { get; private set; }
    public SkillButton SkillButton { get; private set; }

    public void SelectSkill(Skill skill, Stats stats, SkillButton skillButton)
    {
        if(SkillButton)
        {
            SkillButton.CancelSelect();
        }
        SkillButton = skillButton;
        Skill = skill;
        UserStats = stats;
        Raise();
    }
}
