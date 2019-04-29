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
    public DamageEvent damageEvent;

    [TextArea]
    public string description;

    public bool CanUse(Stats userStats)
    {
        return userStats.hp > cost;
    }

    public virtual void OnUse(Stats userStats, Enemy enemy)
    {
        userStats.hp -= cost;
        int damage = enemy.stats.TakeDamage(
                power + ((attribute == SkillAttribute.Magical) ? userStats.intelligence : userStats.strength));
        damageEvent.ShowDamage(damage, enemy.spriteTransform);
    }

    public virtual void OnUse(Stats userStats, List<Enemy> enemies)
    {
        userStats.hp -= cost;
        
        for(int i = 0; i < enemies.Count; i++)
        {
            int damage = enemies[i].stats.TakeDamage(
                power + ((attribute == SkillAttribute.Magical) ? userStats.intelligence : userStats.strength));
            damageEvent.ShowDamage(damage, enemies[i].spriteTransform);
        }
    }
}
