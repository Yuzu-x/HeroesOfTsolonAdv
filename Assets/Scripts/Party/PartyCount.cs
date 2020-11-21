using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCount : MonoBehaviour
{
    public float partyCount;
    public List<PartyMember> memberList = new List<PartyMember>();

    public void Update()
    {
        foreach(PartyMember child in this.GetComponentsInChildren<PartyMember>())
        {
            if(!memberList.Contains(child) && memberList.Count < 5)
            {
                memberList.Add(child);
                partyCount = memberList.Count;
            }
        }

        if (this.GetComponentsInChildren<PartyMember>().Length != partyCount)
        {
           memberList.Clear();
           partyCount = 0;
        }
    }
}
