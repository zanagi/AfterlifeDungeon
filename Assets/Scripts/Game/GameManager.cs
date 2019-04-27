using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public StateChangeEvent stateObject;

    [Header("Player")]
    public PlayerSpawnEvent playerSpawnEvent;
    public Player player;
    
    public void SpawnPlayer()
    {
        Debug.Log("Spawning player: " + playerSpawnEvent.PlayerPos);
        player.transform.position = playerSpawnEvent.PlayerPos;
    }

    public void OnStateChange()
    {
        if(stateObject.CurrentState != GameState.Idle)
        {
            player.Stop();
        }
    }

    private void Update()
    {
        if (stateObject.CurrentState != GameState.Idle)
            return;
        player.HandleUpdate();
    }

    private void FixedUpdate()
    {
        if (stateObject.CurrentState != GameState.Idle)
            return;
        player.HandleFixedUpdate();
    }
}
