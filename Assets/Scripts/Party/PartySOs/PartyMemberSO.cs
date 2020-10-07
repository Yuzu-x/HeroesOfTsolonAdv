using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Member", menuName = "Character/Party Member")]
public class PartyMemberSO : ScriptableObject
{
    public string memberClass;
    public ClassSOBody classSO;
    public float memberLevel;
    public GameObject strategyPiece;
    public GameObject partyMenuPiece;
    public GameObject overworldPiece;
}
