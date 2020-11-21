using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharacterSelect : MonoBehaviour
{
    public GameObject[] playerParty;
    public TurnManager turnManager;

    void Start()
    {
        playerParty = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        if (turnManager.playerTurn)
        {
            playerParty = GameObject.FindGameObjectsWithTag("Player");
        }
    
        if(Input.GetMouseButtonUp(0))
        {
            Ray characterSelect = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit selectHit;

            if(Physics.Raycast(characterSelect, out selectHit))
            {
                if(selectHit.collider.tag == "Player")
                {
                    if(GameObject.FindGameObjectsWithTag("ActivePlayer").Length > 0)
                    {
                        foreach(GameObject activePlayer in GameObject.FindGameObjectsWithTag("ActivePlayer"))
                        {
                            activePlayer.tag = "Player";
                        }
                    }

                    if (selectHit.collider.tag != "ActivePlayer")
                    {
                        selectHit.collider.tag = "ActivePlayer";
                    }
                }
            }
        }
    }
}
