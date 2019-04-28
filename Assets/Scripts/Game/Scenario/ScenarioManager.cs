using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public virtual void PlayScenario(Scenario scenario)
    {
        Debug.LogWarning("No scenario in base scenario manager!");
    }
}
