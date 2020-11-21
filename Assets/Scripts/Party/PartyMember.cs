using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour
{
    public PartyMemberSO memberSO;

    public PartyMemberSO paladinSO;
    public PartyMemberSO knightSO;
    public PartyMemberSO engineerSO;
    public PartyMemberSO priestSO;
    public PartyMemberSO witchSO;
    public PartyMemberSO miasmaticSO;
    public PartyMemberSO assassinSO;
    public PartyMemberSO fighterSO;
    public PartyMemberSO mageSO;
    public PartyMemberSO necromancerSO;
    public PartyMemberSO rangerSO;
    public PartyMemberSO tamerSO;

    public PaladinClass paladin;

    public bool isHired = false;

    private void Update()
    {
        if(this.gameObject.transform.parent.tag == "PartySlot")
        {
            isHired = true;
        }
        else
        {
            isHired = false;
        }
    }

}
