using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractTrigger : MonoBehaviour
{
    private Player player;

    // Use this for initialization
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponentInParent<Interactable>();

        if (interactable)
        {
            player.SetLastInteractable(interactable);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Interactable interactable = other.GetComponentInParent<Interactable>();

        if (interactable)
        {
            player.SetLastInteractable(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponentInParent<Interactable>();

        if (interactable)
        {
            player.ClearLastInteractable(interactable);
        }
    }
}
