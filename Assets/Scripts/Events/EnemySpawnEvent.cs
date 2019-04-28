using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Event for loading a new scene
[CreateAssetMenu(menuName = "Game Event/Enemy Spawn Event")]
public class EnemySpawnEvent : GameEvent
{
    public BaseAI NPCPrefab { get; private set; }
    public Vector3 SpawnPos { get; private set; }

    public void Spawn(BaseAI npcPrefab, Vector3 spawnPos)
    {
        NPCPrefab = npcPrefab;
        SpawnPos = spawnPos;
        Raise();
    }
}
