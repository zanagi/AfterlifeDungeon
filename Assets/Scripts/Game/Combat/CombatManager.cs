using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CombatManager : MonoBehaviour
{
    public CombatEvent combatEvent;
    public StateChangeEvent stateEvent;
    public float startAnimTime = 1.0f;
    
    private void Start()
    {
    }

    public void StartCombat()
    {
        if (stateEvent.ChangeState(GameState.Combat))
        {
            StartCoroutine(Combat());
        }
    }

    private IEnumerator Combat()
    {
        yield return AnimateCombatStart();
    }

    private IEnumerator AnimateCombatStart()
    {
        float time = 0f;

        while(time < startAnimTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }
}
