using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Event for loading a new scene
[CreateAssetMenu(menuName = "Game Event/Load Event")]
public class LoadEvent : GameEvent
{
    public string SceneName { get; private set; }

    public void Load(string sceneName)
    {
        SceneName = sceneName;
        Raise();
    }
}
