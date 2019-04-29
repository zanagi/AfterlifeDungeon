using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Damage Skill (Basic)")]
public class Skill : ScriptableObject
{
    public Sprite sprite;
    public int cost, power;
    public SkillType type;
    public SkillAttribute attribute;
    public bool isAoe;

    [TextArea]
    public string description;

    public bool CanUse(Stats userStats)
    {
        return userStats.hp > cost;
    }

    public virtual void OnUse(Stats userStats, Enemy enemy)
    {
        userStats.hp -= cost;
        enemy.stats.TakeDamage(
                power + ((attribute == SkillAttribute.Magical) ? userStats.intelligence : userStats.strength));
    }

    public virtual void OnUse(Stats userStats, List<Enemy> enemies)
    {
        userStats.hp -= cost;
        
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].stats.TakeDamage(
                power + ((attribute == SkillAttribute.Magical) ? userStats.intelligence : userStats.strength));
        }
    }
}
