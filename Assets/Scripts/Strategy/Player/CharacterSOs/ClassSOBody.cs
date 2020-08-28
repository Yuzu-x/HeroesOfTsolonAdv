using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Class/Empty")]
public class ClassSOBody : ScriptableObject
{
    public GameObject characterGamePiece;

    public float healthPoints;
    public float moveRange;
    public float actionPoints;
    public float basicAttackDamage;
    public float meleeSkill;
    public float rangedSkill;

}
