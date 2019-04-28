using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManagerLevel1 : ScenarioManager
{
    public override void PlayScenario(Scenario scenario)
    {
        switch(scenario)
        {
            case Scenario.Level1_FightA:
                break;
            case Scenario.Level1_FightB:
                break;
        }
    }
}
