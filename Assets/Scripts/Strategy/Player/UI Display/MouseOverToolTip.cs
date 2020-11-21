using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverToolTip : MonoBehaviour
{
    public GameObject toolTipObject;
    public Text toolTipTextBox;
    public GameObject strategyCanvas;

    GraphicRaycaster graphicRaycaster;
    PointerEventData mouseOverUI;
    EventSystem interested;
    public float interestTime;
    public bool hasLoaded = false;

    public void Start()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        interested = GetComponent<EventSystem>();
    }
    void Update()
    {
        mouseOverUI = new PointerEventData(interested);
        mouseOverUI.position = Input.mousePosition;
        List<RaycastResult> checkUIPiece = new List<RaycastResult>();

        graphicRaycaster.Raycast(mouseOverUI, checkUIPiece);

        if (checkUIPiece.Count > 0)
        {
            foreach (RaycastResult pieceFound in checkUIPiece)
            {
                interestTime += 1 * Time.deltaTime;

                if (interestTime >= 3 && !hasLoaded)
                {
                    toolTipObject.SetActive(true);
                    toolTipObject.transform.SetParent(pieceFound.gameObject.transform);
                    
                    toolTipObject.transform.position = new Vector2(pieceFound.gameObject.transform.position.x, pieceFound.gameObject.transform.position.y + 100);
                    hasLoaded = true;
                }
            }
        }
        else
        {
            interestTime = 0f;
            hasLoaded = false;
            toolTipObject.SetActive(false);
        }
    }
}
