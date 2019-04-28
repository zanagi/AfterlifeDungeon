using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Event for loading a new scene
[CreateAssetMenu(menuName = "Game Event/Enemy Spawn Event")]
public class EnemySpawnEvent : GameEvent
{
    public EnemySoul EnemyPrefab { get; private set; }
    public Vector3 SpawnPos { get; private set; }

    public void Spawn(EnemySoul enemyPrefab, Vector3 spawnPos)
    {
        EnemyPrefab = enemyPrefab;
        SpawnPos = spawnPos;
        Raise();
    }
}
