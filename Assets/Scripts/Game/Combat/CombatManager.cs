using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CombatManager : MonoBehaviour
{
    public CombatEvent combatEvent;
    public StateChangeEvent stateEvent;
    public GameObject combatScreen;

    [Header("Start anim")]
    public float startAnimTime = 1.0f, startPause = 0.5f;
    public CanvasGroup startScreen;

    [Header("Combat")]
    public float enemySpacing = 1.0f, enemyDistance = 2.0f;

    [Header("Audio")]
    public BGMEvent bgmEvent;
    public GameEvent bgmRollbackEvent;
    public AudioClip bgm;
    public float bgmVolume = 1.0f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponentInParent<GameManager>();
    }

    public void StartCombat()
    {
        if (stateEvent.ChangeState(GameState.Combat))
        {
            StartCoroutine(Combat());
        }
    }

    private void SetGameObjects(bool active)
    {
        gameManager.player.gameObject.SetActive(!active);
        combatScreen.SetActive(active);
    }

    private IEnumerator Combat()
    {
        // Animate fade-in
        yield return AnimateStartScreen(0, 1);
        bgmEvent.ChangeBGM(bgm, bgmVolume);
        SetGameObjects(true);

        // Create enemies
        List<Enemy> enemies = InitEnemies();

        // Animate fade-out
        yield return new WaitForSecondsRealtime(startPause);
        yield return AnimateStartScreen(1, 0);

        // Combat
        while(enemies.Count > 0)
        {
            yield return null;
        }

        // End
        yield return AnimateStartScreen(0, 1);
        SetGameObjects(false);
        bgmRollbackEvent.Raise();
        yield return AnimateStartScreen(1, 0);
        stateEvent.ChangeState(GameState.Idle);
    }

    private List<Enemy> InitEnemies()
    {
        List<Enemy> enemies = new List<Enemy>();
        Enemy[] enemyPrefabs = combatEvent.EnemyGroup.enemyPrefabs;
        int count = enemyPrefabs.Length;

        for(int i = 0; i < count; i++)
        {
            Enemy enemy = enemyPrefabs[i].Spawn(transform);
            enemy.transform.localPosition = new Vector3(enemySpacing * ((count - 1) / 2 - i), 0, enemyDistance);
            enemies.Add(enemy);
        }
        return enemies;
    }

    private IEnumerator AnimateStartScreen(float a, float b)
    {
        if (a <= 0)
            startScreen.gameObject.SetActive(true);

        float time = 0f;
        while(time < startAnimTime)
        {
            time += Time.deltaTime;
            startScreen.alpha = Mathf.Lerp(a, b, time / startAnimTime);
            yield return null;
        }

        if (b <= 0)
            startScreen.gameObject.SetActive(false);
    }
}
