using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell/New") ]
public class SpellFramework : ScriptableObject
{
    public string spellName;
    public float maximumCoolDown;
    public float actionPointCost;
    public float fatigueCost;
    public float spellRange;
    public float burstRadius;
    public bool meleeSkillCheck = false;
    public bool rangedSkillCheck = false;

    public bool sharedCoolDown = false;

    public float spellDamage;
    public float spellHealing;
    public float spellSummonQuantity;

    public GameObject summonObject;

    public enum SpellType
    { 
        DAMAGE,
        HEALING,
        PERSONAL,
        BOON,
        SUMMON
    }
    public SpellType spellType;

    public enum SpellTargetStyle
    {
        SELECTED,
        LINE,
        BURST,
        BLAST,
        CONE,
        NONE
    }

    public SpellTargetStyle targetStyle;

    public enum CastingStyle
    {
        INSTANT,
        LONGCAST
    }

    public CastingStyle castingStyle;

    public float longCastDuration;
}
