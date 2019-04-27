using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameCamera : MonoBehaviour
{
    public float turnSpeed = 1.0f;

    private void Start()
    {
        SetCursorLock(true);
    }

    public void SetCursorLock(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    public void HandleUpdate()
    {
        var x = CrossPlatformInputManager.GetAxis(Static.mouseXAxis);
        transform.Rotate(Vector3.up * x * turnSpeed);
    }
}
