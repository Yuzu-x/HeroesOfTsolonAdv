using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Character/Enemy/Empty")]
public class EnemySOBody : ScriptableObject
{
    public GameObject enemyGameObject;

    public float healthPoints;
    public float actionPoints;
    public float moveRange;
    public float basicAttackDamage;
    public float meleeSkill;
    public float rangeSkill;

}
