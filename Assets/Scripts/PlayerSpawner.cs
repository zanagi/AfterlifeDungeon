using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.player.transform.position = transform.position;
        GameManager.Instance.player.transform.rotation = transform.rotation;
    }
}
