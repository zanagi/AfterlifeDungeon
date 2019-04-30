using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public LoadEvent loadEvent;
    public StateChangeEvent state;
    public string sceneName = "Level1";
    public float delay = 0.5f;

    private void Awake()
    {
        if (GameManager.Instance)
            Destroy(GameManager.Instance.gameObject);

        if (state)
            state.ChangeState(GameState.Idle);
        else
            enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }

        if(Input.anyKeyDown)
        {
            loadEvent.LoadScene(sceneName);
        } 
    }
}
