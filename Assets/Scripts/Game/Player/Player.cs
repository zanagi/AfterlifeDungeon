using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ContactEvent contactEvent;

    [HideInInspector]
    public Interactable lastInteractable;
    private PlayerComponent[] components;

    private void Start()
    {
        components = GetComponentsInChildren<PlayerComponent>();
    }

    public void HandleUpdate()
    {
        for (int i = 0; i < components.Length; i++)
            components[i].HandleUpdate(this);
    }

    public void HandleFixedUpdate()
    {
        for (int i = 0; i < components.Length; i++)
            components[i].HandleFixedUpdate(this);
    }

    public void Stop()
    {
        for (int i = 0; i < components.Length; i++)
            components[i].Stop(this);
    }


    public void SetLastInteractable(Interactable interactable)
    {
        if (lastInteractable != interactable)
        {
            lastInteractable = interactable;

            if(interactable)
            {
                contactEvent.OnContact(interactable.interactText);
            }
        }
    }

    public void ClearLastInteractable(Interactable interactable)
    {
        if (lastInteractable == interactable)
        {
            lastInteractable = null;
            contactEvent.OnContact(string.Empty);
        }
    }
}
