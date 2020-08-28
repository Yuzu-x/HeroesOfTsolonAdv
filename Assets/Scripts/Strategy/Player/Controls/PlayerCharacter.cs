using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : CharacterController
{
    public ClassSOBody SOInfo;

    public Button moveButton;
    public Button runButton;
    public Button endTurnButton;

    public float interestTime = 0f;

    void Start()
    {
        currentState = TurnState.WAITING;
        maximumHealth = SOInfo.healthPoints;
        currentHealth = maximumHealth;

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
        }

        if(gameObject.tag == "ActivePlayer")
        {
            moveButton.onClick.AddListener(MoveButton);
            runButton.onClick.AddListener(RunButton);
            endTurnButton.onClick.AddListener(EndTurnButton);
            isActive = true;
        }
        else if(gameObject.tag != "ActivePlayer")
        {
            isActive = false;
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

        if(moveActionsThisTurn < 1)
        {
            currentState = TurnState.MOVING;
        }
        else
        {
            moveSelect = false;
            currentState = TurnState.WAITING;
        }
    }

    public void RunButton()
    {
        runSelect = true;
        moveSelect = false;

        runRange -= fatigue;
        currentState = TurnState.RUNNING;
    }

    public void EndTurnButton()
    {
        if(!isMoving)
        {
            gameObject.tag = "Player";
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
