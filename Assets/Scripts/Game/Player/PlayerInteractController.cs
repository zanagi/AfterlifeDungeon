using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInteractController : PlayerComponent
{
    public GameCamera gameCamera;

    public override void HandleUpdate(Player player)
    {
        if(player.lastInteractable && CrossPlatformInputManager.GetButtonDown(Static.mouseClickAxis))
        {
            gameCamera.RotateTowards(player.lastInteractable.transform);
            player.lastInteractable.onInteract.Invoke();
        }
    }
}
