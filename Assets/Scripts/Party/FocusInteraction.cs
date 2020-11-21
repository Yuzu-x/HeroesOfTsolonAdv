using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FocusInteraction : MonoBehaviour
{
    public bool focusMenuActive = false;

    public GameObject focusTreePanel;
    public GameObject partyManager;
    public PartyManagementButtons managementButtons;
    public CharacterLevelling levelling;

    void Update()
    {
        if(focusMenuActive)
        {
            WriteFocus();
        }
        else
        {
            CloseFocus();
        }

        if(managementButtons == null)
        {
            partyManager = GameObject.FindGameObjectWithTag("PartyManager");
            managementButtons = partyManager.GetComponentInChildren<PartyManagementButtons>();
        }
    }

    public void FocusMenu()
    {
        if(!focusMenuActive && managementButtons.focusActive)
        {
            focusMenuActive = true;
        }
    }

    public void CloseFocusMenu()
    {
        if(focusMenuActive)
        {
            focusMenuActive = false;
            levelling.focusingCharacter = null;
        }
    }

    public void WriteFocus()
    {
        focusTreePanel.SetActive(true);
        focusTreePanel.transform.SetParent(partyManager.transform);
        focusTreePanel.transform.position = partyManager.transform.position;

        levelling = focusTreePanel.GetComponent<CharacterLevelling>();
        levelling.focusingCharacter = this.gameObject.GetComponent<PartyMember>();
    }

    public void CloseFocus()
    {
        focusTreePanel.SetActive(false);
    }
}
