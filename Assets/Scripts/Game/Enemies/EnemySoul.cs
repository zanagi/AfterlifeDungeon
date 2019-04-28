using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoul : MonoBehaviour
{
    private BaseAI ai;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<BaseAI>();
    }

    public void HandleUpdate(Player player)
    {
        ai.HandleUpdate(player);
    }

    public void HandleFixedUpdate(Player player)
    {
        ai.HandleFixedUpdate(player);
    }
}
