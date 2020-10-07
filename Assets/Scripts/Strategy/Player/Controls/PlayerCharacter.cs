using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : CharacterController
{
    public ClassSOBody SOInfo;

    public Image[] slotPanel;
    public Image[] panelChildren;
    public Button moveButton;
    public Button runButton;
    public Button endTurnButton;

    public float interestTime = 0f;

    public bool hasClicked = false;
    public bool hasEscaped = false;

    public string spellOneDescription;
    public string spellTwoDescription;
    public string spellThreeDescription;
    public string spellFourDescription;

    void Start()
    {
        currentState = TurnState.WAITING;
        maximumHealth = SOInfo.healthPoints;
        currentHealth = maximumHealth;
        slotPanel = this.GetComponentsInChildren<Image>();

        foreach(Image panel in slotPanel)
        {
            if(panel.gameObject.tag == "UIPartyPanel")
            {
                GameObject partyPanel = GameObject.FindGameObjectWithTag("PartySlot");

                if(panel.GetComponentsInChildren<Image>().Length > 0)
                {
                    panelChildren = panel.GetComponentsInChildren<Image>();
                }

                foreach(Image child in panelChildren)
                {
                    if(child.tag == "PlayerHealth")
                    {
                        Image healthFound = child.GetComponent<Image>();
                        Debug.Log(healthFound + "is healthImage");
                        healthImage = healthFound;
                    }

                    if(child.tag == "PlayerActions")
                    {
                        actionPointImage = child.GetComponent<Image>();
                    }
                }

                panel.transform.SetParent(partyPanel.transform);
            }
        }

        Init();
    }

    void Update()
    {
        if(turnManager.playerTurn)
        {
            myTurn = true;
        }
        else if(turnManager.enemyTurn)
        {
            myTurn = false;
            hasClicked = false;
        }

        if(moveButton == null)
        {
            GameObject moveButtonObject = GameObject.FindGameObjectWithTag("MoveButton");
            GameObject runButtonObject = GameObject.FindGameObjectWithTag("RunButton");
            GameObject endTurnButtonObject = GameObject.FindGameObjectWithTag("EndTurnButton");
            moveButton = moveButtonObject.GetComponent<Button>();
            runButton = runButtonObject.GetComponent<Button>();
            endTurnButton = endTurnButtonObject.GetComponent<Button>();
        }

        if(tag == "ActivePlayer")
        {
            moveButton.onClick.AddListener(MoveButton);
            runButton.onClick.AddListener(RunButton);
            endTurnButton.onClick.AddListener(EndTurnButton);
            isActive = true;
        }
        else if(tag != "ActivePlayer" || currentActionPoints <= 0)
        {
            isActive = false;
            moveButton.onClick.RemoveListener(MoveButton);
            runButton.onClick.RemoveListener(RunButton);
        }
        else if(myTurn && GameObject.FindGameObjectsWithTag("ActivePlayer").Length < 1)
        {
            moveButton.enabled = false;
        }

        MouseOverInteractable();

        switch (currentState)
        {
            case (TurnState.MOVING):
                moveRange = SOInfo.moveRange;
                MoveSelected();
                break;

            case (TurnState.RUNNING):
                moveRange = runRange;
                MoveSelected();
                break;

            case (TurnState.CASTING):
                break;

            case (TurnState.LONGCASTING):
                break;

            case (TurnState.WAITING):
                break;

            case (TurnState.DEAD):
                break;
        }

        if(currentActionPoints == 0)
        {
            if(currentState != TurnState.LONGCASTING)
            {
                currentState = TurnState.WAITING;
                gameObject.tag = "Player";
            }
        }

        actionPointImage.fillAmount = currentActionPoints / maximumActionPoints;
        healthImage.fillAmount = currentHealth / maximumHealth;
    }

    void MoveSelected()
    {
        if(isActive && !isMoving)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            CharacterMove();
        }
    }

    void CheckMouse()
    {
        Ray pathVisRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit visHit;

        if(Physics.Raycast(pathVisRay, out visHit))
        {
            if(visHit.collider.tag == "Tile")
            {
                TileController tileHit = visHit.collider.GetComponent<TileController>();

                if(tileHit.selectableTile)
                {
                    tileHit.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit clickHit;

            if(Physics.Raycast(clickRay, out clickHit))
            {
                if(clickHit.collider.tag == "Tile")
                {
                    TileController clickTile = clickHit.collider.GetComponent<TileController>();

                    if(clickTile.selectableTile)
                    {
                        MoveToTile(clickTile);
                    }
                }
            }
        }
    }

    public void MoveButton()
    {
        moveSelect = true;
        runSelect = false;

        if(this.moveActionsThisTurn < 1 && this.currentActionPoints > 0)
        {
            this.currentState = TurnState.MOVING;
        }
        else
        {
            moveSelect = false;
            currentState = TurnState.WAITING;
        }
    }

    public void RunButton()
    {
        if (!runSelect && currentActionPoints > 0 && isActive)
        {
            runSelect = true;
            moveSelect = false;

            if(fatigue > 0)
            {
                runRange = 6 - fatigue;
            }

            currentState = TurnState.RUNNING;
        }
    }

    public void EndTurnButton()
    {
        if(!isMoving && !hasClicked)
        {
            hasClicked = true;
            gameObject.tag = "Player";
            BasicAttack();
            isActive = false;
            turnManager.playerTurn = false;
            turnManager.enemyTurn = true;
            turnManager.turnCount += 1f;
            moveActionsThisTurn = 0f;
            turnManager.FinishTurn();
        }
    }

    void MouseOverInteractable()
    {
        Ray mouseSearch = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit canInteract;

        if(Physics.Raycast(mouseSearch, out canInteract))
        {

        }
    }
}
