using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharacterSelect : MonoBehaviour
{
    public GameObject[] playerParty;

    void Start()
    {
        playerParty = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
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
                    selectHit.collider.tag = "ActivePlayer";
                }
            }
        }
    }
}
