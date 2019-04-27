using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Event for loading a new scene
[CreateAssetMenu(menuName = "Game Event/Player Spawn Event")]
public class PlayerSpawnEvent : GameEvent
{
    public Vector3 PlayerPos { get; private set; }

    public void Spawn(Vector3 playerPos)
    {
        PlayerPos = playerPos;
        Raise();
    }
}
