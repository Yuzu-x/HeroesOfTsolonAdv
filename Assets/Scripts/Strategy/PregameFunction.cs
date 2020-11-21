using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PregameFunction : MonoBehaviour
{
    public TurnManager turnManager;
    public GameObject preGameCanvas;
    public List<PartyMember> partyMembers = new List<PartyMember>();

    public UIPieceAction pieceAction;
    public GameObject characterGamePiece;
    public PartyMember heldPartyMember;
    public bool hasCreatedPiece = false;

    public Image characterHandImage;
    public GameObject gamePiece1;
    public bool pieceOneLoaded = false;
    public GameObject gamePiece2;
    public bool pieceTwoLoaded = false;
    public GameObject gamePiece3;
    public bool pieceThreeLoaded = false;
    public GameObject gamePiece4;
    public bool pieceFourLoaded = false;
    public GameObject gamePiece5;
    public bool pieceFiveLoaded = false;

    void Start()
    {
        GameObject[] findPMs = GameObject.FindGameObjectsWithTag("PartyHandPiece");
        foreach(GameObject member in findPMs)
        {
            partyMembers.Add(member.GetComponent<PartyMember>());
        }
    }

    void Update()
    {
        if(turnManager.currentlyTurnZero)
        {
            turnManager.playerTurn = false;
            turnManager.enemyTurn = false;
            preGameCanvas.SetActive(true);
        }
        else
        {
            preGameCanvas.SetActive(false);
        }

        if(pieceAction.heldPiece != null)
        {
            characterHandImage = pieceAction.heldPiece.gameObject.GetComponent<Image>();

            Ray lookForSpawn = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit checkSpawnSearch;

            PointerEventData mouseOverUI = new PointerEventData(EventSystem.current);
            mouseOverUI.position = Input.mousePosition;
            List<RaycastResult> findHandSlots = new List<RaycastResult>();
            EventSystem.current.RaycastAll(mouseOverUI, findHandSlots);

            if(Physics.Raycast(lookForSpawn, out checkSpawnSearch))
            {
                if(checkSpawnSearch.collider.tag == "Tile")
                {
                   TileController spawnTile = checkSpawnSearch.collider.GetComponent<TileController>();
                   heldPartyMember = pieceAction.heldPiece.GetComponent<PartyMember>();

                    if (pieceAction.heldPiece != null)
                    {
                        characterGamePiece = heldPartyMember.memberSO.strategyPiece;
                    }

                    characterHandImage.enabled = false;

                    if(spawnTile.playerSpawnTile)
                    {
                        spawnTile.GetComponent<Renderer>().material.color = Color.cyan;

                        if(Input.GetMouseButtonUp(0))
                        {
                            pieceAction.heldPiece = null;

                                if (!pieceOneLoaded && gamePiece1 == null)
                                {
                                    gamePiece1 = Instantiate(characterGamePiece); 
                                    gamePiece1.transform.SetParent(spawnTile.transform);
                                    gamePiece1.transform.position = new Vector3(spawnTile.transform.position.x, spawnTile.transform.position.y + 1, spawnTile.transform.position.z);
                                    gamePiece1.transform.rotation = spawnTile.transform.rotation;
                                gamePiece1.transform.SetParent(null);
                                pieceOneLoaded = true;
                                }
                                else if(!pieceTwoLoaded && gamePiece1 != null && gamePiece2 == null)
                                {
                                    gamePiece2 = Instantiate(characterGamePiece);
                                    gamePiece2.transform.SetParent(spawnTile.transform);
                                    gamePiece2.transform.position = new Vector3(spawnTile.transform.position.x, spawnTile.transform.position.y + 1, spawnTile.transform.position.z);
                                    gamePiece2.transform.rotation = spawnTile.transform.rotation;
                                gamePiece2.transform.SetParent(null);
                                pieceTwoLoaded = true;
                                }
                                else if(!pieceThreeLoaded && gamePiece1 != null && gamePiece2 != null && gamePiece3 == null)
                                {
                                    gamePiece3 = Instantiate(characterGamePiece);
                                    gamePiece3.transform.SetParent(spawnTile.transform);
                                    gamePiece3.transform.position = new Vector3(spawnTile.transform.position.x, spawnTile.transform.position.y + 1, spawnTile.transform.position.z);
                                    gamePiece3.transform.rotation = spawnTile.transform.rotation;
                                gamePiece3.transform.SetParent(null);
                                pieceThreeLoaded = true;
                                }
                                else if(!pieceFourLoaded && gamePiece1 != null && gamePiece2 != null && gamePiece3 != null && gamePiece4 == null)
                                {
                                    gamePiece4 = Instantiate(characterGamePiece);
                                    gamePiece4.transform.SetParent(spawnTile.transform);
                                    gamePiece4.transform.position = new Vector3(spawnTile.transform.position.x, spawnTile.transform.position.y + 1, spawnTile.transform.position.z);
                                    gamePiece4.transform.rotation = spawnTile.transform.rotation;
                                gamePiece4.transform.SetParent(null);
                                pieceFourLoaded = true;
                                }
                                else if(!pieceFiveLoaded && gamePiece1 != null && gamePiece2 != null && gamePiece3 != null && gamePiece4 != null && gamePiece5 == null)
                                {
                                    gamePiece5 = Instantiate(characterGamePiece);
                                    gamePiece5.transform.SetParent(spawnTile.transform);
                                    gamePiece5.transform.position = new Vector3(spawnTile.transform.position.x, spawnTile.transform.position.y + 1, spawnTile.transform.position.z);
                                    gamePiece5.transform.rotation = spawnTile.transform.rotation;
                                gamePiece5.transform.SetParent(null);
                                pieceFiveLoaded = true;
                            }

                            characterHandImage.gameObject.SetActive(false);
                            spawnTile.playerSpawnTile = false;
                        }
                    }
                }
                else
                {
                    characterHandImage.enabled = true;
                }
            }
            else
            {
                characterHandImage.enabled = true;

                foreach (RaycastResult results in findHandSlots)
                {
                    if(results.gameObject.tag != "Tile")
                    {
                        if(Input.GetMouseButtonUp(0))
                        {
                            pieceAction.heldPiece.transform.SetParent(results.gameObject.transform);
                            pieceAction.heldPiece.transform.position = results.gameObject.transform.position;
                            pieceAction.heldPiece = null;
                        }
                    }
                    else if(results.gameObject.tag == "Tile")
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            pieceAction.heldPiece.transform.SetParent(results.gameObject.transform);
                            pieceAction.heldPiece.transform.position = results.gameObject.transform.position;
                            pieceAction.heldPiece = null;
                        }
                    }
                }
            }
        }
    }
}
