using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIReposition : MonoBehaviour
{
    public Transform partyPanel;
    GameObject heldPiece;
    GameObject[] availableRecruits;
    public GameObject[] partySlots;
    Transform inParty;

    public bool overParty = false;
    public bool overRecruit = false;

    public List<Transform> partyTransforms = new List<Transform>();
    List<GameObject> recruitObjects = new List<GameObject>();
    public List<GameObject> partyObjects = new List<GameObject>();
    public Image recruitPanel;
    public Transform recruitRosterPos;
    public GraphicRaycaster imageRaycaster;

    public UIPieceAction actionable;

    void Start()
    {
        availableRecruits = GameObject.FindGameObjectsWithTag("RecruitTab");
        recruitRosterPos = recruitPanel.transform;
        imageRaycaster = GetComponent<GraphicRaycaster>();

        partySlots = GameObject.FindGameObjectsWithTag("PartySlot");

        foreach(GameObject ps in partySlots)
        {
            inParty = ps.GetComponentInChildren<Transform>();
            partyTransforms.Add(inParty);
        }
    }

    void Update()
    {
        PointerEventData mouse = new PointerEventData(EventSystem.current);
        mouse.position = Input.mousePosition;
        List<RaycastResult> findPartySlots = new List<RaycastResult>();
        imageRaycaster.Raycast(mouse, findPartySlots);
        //EventSystem.current.RaycastAll(mouse, findPartySlots);
        int count = findPartySlots.Count;

        foreach(GameObject recruit in availableRecruits)
        {
            recruitObjects.Add(recruit);
        }

        foreach(RaycastResult res in findPartySlots)
        {
            if(res.gameObject.tag == "PartySlot")
            {
                overParty = true;
            }
            else if(res.gameObject.tag == "RecruitTab")
            {
                overRecruit = true;
            }
            else
            {
                overParty = false;
                overRecruit = false;
            }

            if(actionable.heldPiece)
            {
                Image recruitImage = actionable.heldPiece.GetComponent<Image>();
                recruitImage.raycastTarget = false;

                foreach(Transform emptySlot in partyTransforms)
                {
                    if(Input.GetMouseButtonUp(0))
                    {
                        if (partyObjects.Count < 5)
                        {
                            actionable.heldPiece.transform.SetParent(emptySlot);
                            recruitObjects.Remove(emptySlot.gameObject);
                            partyObjects.Add(emptySlot.gameObject);

                            actionable.heldPiece.transform.position = emptySlot.transform.position;
                            actionable.heldPiece.transform.rotation = emptySlot.transform.rotation;

                            actionable.heldPiece = null;
                        }
                        else
                        {
                            actionable.heldPiece.transform.SetParent(recruitPanel.transform);
                            actionable.heldPiece.transform.position = recruitPanel.transform.position;
                            actionable.heldPiece = null;
                        }
                    }
                    else if(Input.GetMouseButtonUp(0) && !overParty)
                    {
                        actionable.heldPiece.transform.SetParent(recruitPanel.transform);
                        actionable.heldPiece.transform.position = recruitPanel.transform.position;
                        actionable.heldPiece = null;
                    }
                }
            }
        }
    }
}
