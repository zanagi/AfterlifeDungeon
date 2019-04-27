using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : PlayerComponent
{
    public float speed;
    private Rigidbody rBody;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void HandleUpdate(Player player)
    {
        rBody.velocity = new Vector3(Input.GetAxis(Static.horizontalAxis), 0, Input.GetAxis(Static.verticalAxis)) * speed;
    }
}
