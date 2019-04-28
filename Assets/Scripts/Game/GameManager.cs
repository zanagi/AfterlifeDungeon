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

    [Header("Enemy")]
    public EnemySpawnEvent enemySpawnEvent;
    private List<EnemySoul> enemies;

    public void SpawnPlayer()
    {
        player.transform.position = playerSpawnEvent.PlayerPos;
    }

    public void SpawnEnemy()
    {
        if (enemies == null)
            enemies = new List<EnemySoul>();
        EnemySoul enemy = Instantiate(enemySpawnEvent.EnemyPrefab);
        enemy.transform.position = enemySpawnEvent.SpawnPos;
        enemies.Add(enemy);
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
