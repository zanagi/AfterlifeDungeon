using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInteractController : PlayerComponent
{
    public override void HandleUpdate(Player player)
    {
        if(player.lastInteractable && CrossPlatformInputManager.GetButtonDown(Static.mouseClickAxis))
        {
            player.lastInteractable.onInteract.Invoke();
        }
    }
}
