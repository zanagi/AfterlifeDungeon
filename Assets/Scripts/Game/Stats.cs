using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public Sprite sprite;
    public List<Skill> skills;

    public int level;
    public int exp, nextLvlExp;

    public int hp, maxHp;
    public int strength, vitality, intelligence, agility;
    
    public Stats(Sprite sprite, List<Skill> skills, int level, int exp, int nextLvlExp, int hp, int maxHp,
        int strength, int vitality, int intelligence, int agility)
    {
        this.sprite = sprite;
        this.skills = skills;
        this.level = level;
        this.exp = exp;
        this.nextLvlExp = nextLvlExp;
        this.hp = hp;
        this.maxHp = maxHp;
        this.strength = strength;
        this.vitality = vitality;
        this.intelligence = intelligence;
        this.agility = agility;
    }

    public Stats Clone()
    {
        return new Stats(sprite, skills, level, exp, nextLvlExp, hp, maxHp, strength, vitality, intelligence, agility);
    }

    public float HealthRatio
    {
        get { return (float)hp / Mathf.Max(1, maxHp); }
    }

    public int TakeDamage(int power)
    {
        int damage = (power - vitality);
        damage = Mathf.Max(1, Random.Range(damage / 2, damage));
        hp -= damage;
        return damage;
    }

    public void PayHealth(int amount)
    {
        hp -= amount;
    }

    public void CheckLevelUp()
    {
        if(exp >= nextLvlExp)
        {
            exp -= nextLvlExp;
            level++;
            nextLvlExp = 10 * level;

            // Stats:
            hp += 10;
            maxHp += 10;
        }
    }
}
