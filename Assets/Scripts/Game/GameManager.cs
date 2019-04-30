using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public StateChangeEvent stateObject;

    [Header("Player")]
    public PlayerSpawnEvent playerSpawnEvent;
    public Player player;

    [Header("Enemy")]
    public EnemySpawnEvent enemySpawnEvent;
    public List<BaseAI> NPCs;

    private void Awake()
    {
        if(Instance)
        {
            Instance.NPCs = NPCs;
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnPlayer()
    {
        player.transform.position = playerSpawnEvent.PlayerPos;
    }

    public void SpawnEnemy()
    {
        BaseAI npc = Instantiate(enemySpawnEvent.NPCPrefab);
        npc.transform.position = enemySpawnEvent.SpawnPos;
        NPCs.Add(npc);
    }

    public void OnStateChange()
    {
        if(stateObject.CurrentState != GameState.Idle)
        {
            for (int i = 0; i < NPCs.Count; i++)
                NPCs[i].Stop();
            player.Stop();
        }
    }

    private void Update()
    {
        if (stateObject.CurrentState != GameState.Idle)
            return;

        for (int i = NPCs.Count - 1; i >= 0; i--)
        {
            if (!NPCs[i])
            {
                NPCs.RemoveAt(i);
                continue;
            }
            NPCs[i].HandleUpdate(player);
        }
        player.HandleUpdate();
    }

    private void FixedUpdate()
    {
        if (stateObject.CurrentState != GameState.Idle)
            return;

        for (int i = NPCs.Count - 1; i >= 0; i--)
        {
            if (!NPCs[i])
            {
                NPCs.RemoveAt(i);
                continue;
            }
            NPCs[i].HandleFixedUpdate(player);
        }
        player.HandleFixedUpdate();
    }
}
