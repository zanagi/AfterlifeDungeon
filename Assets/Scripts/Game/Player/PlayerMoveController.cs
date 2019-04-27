using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMoveController : PlayerComponent
{
    public float speed;
    public GameCamera gameCamera;
    private Rigidbody rBody;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void HandleUpdate(Player player)
    {
        HandleMove();
        HandleCamera();
    }
    
    private void HandleMove()
    {
        rBody.velocity = gameCamera.transform.localRotation * new Vector3(
            CrossPlatformInputManager.GetAxis(Static.horizontalAxis), 0,
            CrossPlatformInputManager.GetAxis(Static.verticalAxis)) * speed;
    }

    private void HandleCamera()
    {
        gameCamera.HandleUpdate();
    }

    public override void Stop(Player player)
    {
        rBody.velocity = Vector3.zero;
    }
}
