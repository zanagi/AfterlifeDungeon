using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{
    public CombatEvent combatEvent;
    public EnemyGroup enemyGroup;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            combatEvent.StartCombat(enemyGroup);
        }
    }
}
