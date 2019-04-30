using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public DungeonGenerator generator;
    public string tempScene;

    // Start is called before the first frame update
    void Awake()
    {
        Static.dungeonCount--;

        if (Static.dungeonCount > 0)
            generator.nextLevel = tempScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
