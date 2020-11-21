using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIDragAndDrop : MonoBehaviour
{
    private bool isHeld = false;
    private bool overPartyPanel = false;
    private Vector2 startingPos;
    private GameObject partyPanel;
    private GameObject generatedPanel;

    void Update()
    {
        if(isHeld)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        overPartyPanel = true;
        if (collision.collider.tag == "PartySlot")
        {
            partyPanel = collision.gameObject;
        }
        else if (collision.collider.tag == "UIPartyPanel")
        {
            generatedPanel = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        overPartyPanel = false;
        partyPanel = null;
        generatedPanel = null;
    }

    public void InitiateDrag()
    {
        startingPos = transform.position;
        isHeld = true;
    }

    public void ConcludeDrag()
    {
        isHeld = false;

        if(overPartyPanel && partyPanel != null)
        {
            PartyCount partyCountPanel = partyPanel.GetComponent<PartyCount>();

            if (partyCountPanel.partyCount < 5)
            {
                transform.SetParent(partyPanel.transform, false);
            }
            else
            {
                transform.position = startingPos;
            }
        }
        else if(overPartyPanel && generatedPanel != null)
        {
            transform.SetParent(generatedPanel.transform, false);
        }
        else
        {
            transform.position = startingPos;
        }
    }
}
