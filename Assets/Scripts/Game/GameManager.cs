using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public PlayerSpawnEvent playerSpawnEvent;
    public Transform playerTransform;

    public void SpawnPlayer()
    {
        Debug.Log("Spawning player: " + playerSpawnEvent.PlayerPos);
        playerTransform.position = playerSpawnEvent.PlayerPos;
    }
}
