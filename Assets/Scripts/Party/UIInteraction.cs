using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInteraction : MonoBehaviour
{
    GraphicRaycaster graphicRaycaster;
    PointerEventData mouseOverUI;
    EventSystem interaction;
    public UIPieceAction actionable;

    void Start()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        interaction = GetComponent<EventSystem>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mouseOverUI = new PointerEventData(interaction);
            mouseOverUI.position = Input.mousePosition;

            List<RaycastResult> interactablePieces = new List<RaycastResult>();

            graphicRaycaster.Raycast(mouseOverUI, interactablePieces);

            foreach(RaycastResult piece in interactablePieces)
            {
                if(piece.gameObject.tag == "RecruitTab")
                {
                    actionable.heldPiece = piece.gameObject;
                }
                else if(piece.gameObject.tag == "PartyHandPiece" || piece.gameObject.tag == "CharInHand")
                {
                    actionable.heldPiece = piece.gameObject;
                }
            }
        }
    }
}
