using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyManagementButtons : MonoBehaviour
{
    public GameObject recruitmentTab;
    public GameObject currentPartyPanel;
    public bool recruitActive = true;
    public GameObject partyManagementTab;
    public bool partyManageActive = false;
    public GameObject focusTreeTab;
    public bool focusActive = false;

    public void Update()
    { }

    public void RecruitmentTabButton()
    {
        if(!recruitActive)
        {
            recruitmentTab.SetActive(true);
            recruitActive = true;
            currentPartyPanel.transform.SetParent(recruitmentTab.transform);
            partyManagementTab.SetActive(false);
            partyManageActive = false;
            focusTreeTab.SetActive(false);
            focusActive = false;

        }
    }

    public void PartyManagementButton()
    {
        if(!partyManageActive)
        {
            partyManagementTab.SetActive(true);
            partyManageActive = true;
            currentPartyPanel.transform.SetParent(partyManagementTab.transform);
            recruitmentTab.SetActive(false);
            recruitActive = false;
            focusTreeTab.SetActive(false);
            focusActive = false;
        }
    }

    public void FocusTreeButton()
    {
        if(!focusActive)
        {
            focusTreeTab.SetActive(true);
            focusActive = true;
            currentPartyPanel.transform.SetParent(focusTreeTab.transform);
            recruitmentTab.SetActive(false);
            recruitActive = false;
            partyManagementTab.SetActive(false);
            partyManageActive = false;
        }
    }
}
