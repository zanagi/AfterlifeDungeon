using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ContactEvent contactEvent;
    public Stats stats;

    [HideInInspector]
    public Interactable lastInteractable;
    private PlayerComponent[] components;

    [Header("Game Over")]
    public LoadEvent loadEvent;
    public string gameOverScene;

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
        if (lastInteractable == interactable || interactable == null)
        {
            lastInteractable = null;
            contactEvent.OnContact(string.Empty);
        }
    }

    public void PayHealth(int amount)
    {
        stats.PayHealth(amount);

        if(stats.hp <= 0)
        {
            loadEvent.LoadScene(gameOverScene);
        }
    }
}
