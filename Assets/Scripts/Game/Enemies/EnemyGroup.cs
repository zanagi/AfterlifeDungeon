using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Group")]
public class EnemyGroup : ScriptableObject
{
    public Enemy[] enemyPrefabs;
}
