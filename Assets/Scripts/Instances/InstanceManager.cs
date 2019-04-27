using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager : MonoBehaviour
{
    public StateChangeEvent stateChangeEvent;
    private static InstanceManager Instance;

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Set starting state as idle
        stateChangeEvent.ChangeState(GameState.Idle);
    }
}
