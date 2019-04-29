using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public CombatEvent combatEvent;
    private EnemySoul enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<EnemySoul>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        
        if (player)
        {
            combatEvent.StartCombat(enemy.enemyGroups.GetRandom(), enemy.gameObject);
        }
    }
}
