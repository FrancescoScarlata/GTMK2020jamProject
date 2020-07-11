using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Infos about the enemy. This way i can change the value per different enemy
/// </summary>
[CreateAssetMenu(fileName = "newEnemy", menuName = "Enemy/Info")]
public class EnemyInfos : ScriptableObject
{
    public float HP=3;
    public float movSpeed=2;
    public float damage=1;
    public float aggroRange;
    public EnemyType myType;



}

public enum EnemyType
{
    normal,
    fast,
    tanky
}
