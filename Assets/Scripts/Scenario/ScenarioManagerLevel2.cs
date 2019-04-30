using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManagerLevel2 : ScenarioManager
{
    public EnemyGroup enemyGroupClown;
    public LevelPortal portal;
    public Dialogue portalDialogue;
    public Stats memberStats;
    public GameObject npc;
    public ContactEvent contactEvent;
    private int count;

    public override void PlayScenario(Scenario scenario)
    {
        switch(scenario)
        {
            case Scenario.Level2_Join:
                portal.blockDialogue = portalDialogue;
                GameManager.Instance.GetComponentInChildren<CombatManager>().AddMember(memberStats);
                Destroy(npc);
                contactEvent.OnContact(string.Empty);
                break;
            case Scenario.Level2_ClownFight:
                portal.blockDialogue = null;
                combatEvent.StartCombat(enemyGroupClown, null);
                break;
        }
    }
}
