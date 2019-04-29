using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DungeonCameraEvent : GameEvent
{
    public Vector3 CameraPos { get; private set; }

    public void SetCamera(Vector3 pos)
    {
        CameraPos = pos;
        Raise();
    }
}
