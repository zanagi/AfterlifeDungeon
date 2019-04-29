using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CombatManager : MonoBehaviour
{
    public CombatEvent combatEvent;
    public StateChangeEvent stateEvent;
    public GameObject combatUI, combatScreen;

    [Header("Start anim")]
    public float startAnimTime = 1.0f, startPause = 0.5f;
    public CanvasGroup startScreen;

    [Header("Combat")]
    public GameEvent cursorLockEvent;
    public GameEvent cursorUnlockEvent;
    public float enemySpacing = 1.0f, enemyDistance = 2.0f, turnPause = 1.0f;
    public SkillDescriptionPanel descriptionPanel;
    public SkillSelectEvent skillSelectEvent;
    public HitEffect hitEffect;
    public DamageText damageTextPrefab;
    public Transform uiTop;
    public DamageEvent damageEvent;

    [Header("Combat - Raycast")]
    public Camera combatCamera;
    public LayerMask enemyLayer;
    public float cameraCloseDistance = 0.5f, closeInTime = 0.1f;
    private Skill activeSkill;

    [Header("Audio")]
    public BGMEvent bgmEvent;
    public GameEvent bgmRollbackEvent, hitEvent;
    public AudioClip bgm;
    public float bgmVolume = 1.0f;

    [Header("Game over")]
    public LoadEvent loadEvent;
    public string gameOverScene;

    private GameManager gameManager;
    private PartyMemberPanel[] partyPanels;

    private void Start()
    {
        gameManager = GetComponentInParent<GameManager>();

        // Set party UI
        partyPanels = combatUI.GetComponentsInChildren<PartyMemberPanel>(true);
        for(int i = 0; i < partyPanels.Length; i++)
        {
            if (i == 0)
                partyPanels[i].SetStats(gameManager.player.stats);
            else
                partyPanels[i].SetStats(null);
        }
        combatUI.SetActive(false);
        hitEffect.gameObject.SetActive(false);
    }

    public void StartCombat()
    {
        if (stateEvent.ChangeState(GameState.Combat))
        {
            StartCoroutine(Combat());
        }
    }

    public void SetDescription()
    {
        if(skillSelectEvent.Skill)
            descriptionPanel.SetDescription(skillSelectEvent.Skill.description);
        else
            descriptionPanel.SetDescription(string.Empty);
    }

    private void SetGameObjects(bool active)
    {
        gameManager.player.gameObject.SetActive(!active);
        combatUI.SetActive(active);
        combatScreen.SetActive(active);
    }

    private IEnumerator Combat()
    {
        // Animate fade-in
        yield return AnimateStartScreen(0, 1);
        cursorUnlockEvent.Raise();
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
            yield return PlayTurn(enemies);
            yield return new WaitForSecondsRealtime(turnPause);
            yield return EnemyTurn(enemies);
        }

        // End
        yield return AnimateStartScreen(0, 1);
        SetGameObjects(false);
        bgmRollbackEvent.Raise();
        cursorLockEvent.Raise();

        // Destroy enemy object
        if (combatEvent.EnemyObject)
        {
            gameManager.player.ClearLastInteractable(null);
            Destroy(combatEvent.EnemyObject);
        }

        yield return AnimateStartScreen(1, 0);
        stateEvent.ChangeState(GameState.Idle);
    }
    
    private IEnumerator PlayTurn(List<Enemy> enemies)
    {
        for(int i = 0; i < partyPanels.Length; i++)
        {
            if (partyPanels[i].userStats == null)
                continue;

            partyPanels[i].SetActive(true);

            bool actionPending = true;
            while(actionPending)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;

                    if(Physics.Raycast(combatCamera.ScreenPointToRay(Input.mousePosition), out hit, 10.0f, enemyLayer))
                    {
                        Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

                        if (enemy)
                        {
                            actionPending = false;
                            SetUIVisible(false);

                            Skill skill = skillSelectEvent.Skill;
                            if (skill.isAoe)
                            {
                                skill.OnUse(skillSelectEvent.UserStats, enemies);
                            }
                            else
                            {
                                skill.OnUse(skillSelectEvent.UserStats, enemy);
                                yield return _AnimateAttackCamera(enemy.spriteTransform);
                            }
                            SetUIVisible(true);
                        }
                    }
                }
                yield return null;
            }
            CheckEnemies(enemies);
        }
    }

    private IEnumerator EnemyTurn(List<Enemy> enemies)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            Stats stats = enemies[i].stats;
            List<Skill> skills = stats.skills;

            if (skills.Count > 0)
            {
                Skill skill = skills[Random.Range(0, skills.Count)];
                int playerCount = 1;

                for(int j = 1; j < partyPanels.Length; j++)
                {
                    if (partyPanels[j].userStats != null)
                        playerCount++;
                }
                PartyMemberPanel panel = partyPanels[Random.Range(0, playerCount)];
                skill.OnUse(stats, panel);
                yield return _AnimateAttackCamera(panel.transform, true);
                CheckPlayers();
            }
        }
    }

    private void CheckEnemies(List<Enemy> enemies)
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i].stats.hp <= 0)
            {
                AddPartyExp(enemies[i].stats.exp);
                enemies[i].Recycle();
                enemies.RemoveAt(i);
            }
        }
    }

    private void CheckPlayers()
    {
        for (int i = 0; i < partyPanels.Length; i++)
        {
            Stats stats = partyPanels[i].userStats;
            if (stats != null && stats.hp <= 0)
            {
                if (i == 0)
                {
                    loadEvent.LoadScene(gameOverScene);
                }
                else
                {
                    partyPanels[i].SetStats(null);
                }
            }
        }
    }

    private void AddPartyExp(int amount)
    {
        for(int i = 0; i < partyPanels.Length; i++)
        {
            if (partyPanels[i].userStats != null)
                partyPanels[i].userStats.AddExperience(amount);
        }
    }

    private void SetUIVisible(bool visible)
    {
        for(int i = 0; i < partyPanels.Length; i++)
        {
            partyPanels[i].gameObject.SetActive(visible);
        }
        descriptionPanel.SetActive(visible);
    }

    private IEnumerator _AnimateAttackCamera(Transform targetTransform, bool screenSpace = false)
    {
        Vector3 startPos = combatCamera.transform.position;
        Vector3 targetPos = targetTransform.position - Vector3.forward * cameraCloseDistance;

        float time = 0f;
        if (!screenSpace)
        {
            while (time < closeInTime)
            {
                time += Time.deltaTime;
                combatCamera.transform.position = Vector3.Slerp(startPos, targetPos, time / closeInTime);
                yield return null;
            }
        }

        // Play hit effect
        hitEffect.gameObject.SetActive(true);
        hitEvent.Raise();
        yield return hitEffect._Play(combatCamera, targetTransform, screenSpace);
        hitEffect.gameObject.SetActive(false);
        
        if (!screenSpace)
        {
            time = 0f;
            while (time < closeInTime)
            {
                time += Time.deltaTime;
                combatCamera.transform.position = Vector3.Slerp(targetPos, startPos, time / closeInTime);
                yield return null;
            }
        }
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

    public void ShowDamage()
    {
        // Play damage text
        DamageText damageText = damageTextPrefab.Spawn(uiTop);
        damageText.Play(damageEvent.Damage, combatCamera, damageEvent.Target, damageEvent.IsScreenSpace);
    }
}
