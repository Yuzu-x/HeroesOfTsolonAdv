using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    public GameObject target;

    public EnemySOBody enemyScriptable;

    void Start()
    {
        currentState = TurnState.WAITING;
        maximumHealth = enemyScriptable.healthPoints;
        currentHealth = maximumHealth;
        Init();
    }

    void Update()
    {
        switch(currentState)
        {
            case (TurnState.MOVING):
                EnemyMovement();
                break;

            case (TurnState.RUNNING):

                break;

            case (TurnState.WAITING):
                ShouldEndTurn();
                break;

            case (TurnState.CASTING):

                break;

            case (TurnState.LONGCASTING):

                break;

            case (TurnState.DEAD):

                break;
        }

        if(turnManager.enemyTurn)
        {
            myTurn = true;
        }
        else if(turnManager.playerTurn)
        {
            myTurn = false;
        }

        if(myTurn && currentActionPoints > 0 && moveActionsThisTurn == 0)
        {
            moveSelect = true;

            if(!isMoving)
            {
                currentState = TurnState.MOVING;
            }
        }

        //actionPointImage.fillAmount = currentActionPoints / maximumActionPoints;
        //healthImage.fillAmount = currentHealth / maximumHealth;
    }

    void CalculatePath()
    {
        TileController targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        GameObject nearest = null;
        float distance = Mathf.Infinity;

        if (target == null)
        {
            foreach (GameObject player in targets)
            {
                PlayerCharacter pChara = player.GetComponent<PlayerCharacter>();

                if (pChara.hasTaunted)
                {
                    target = player;
                }
            }

            if (target == null)
            {
                foreach (GameObject player in targets)
                {
                    float currentDistance = Vector3.Distance(transform.position, player.transform.position);

                    if (currentDistance < distance)
                    {
                        distance = currentDistance;
                        nearest = player;
                    }

                    target = nearest;
                }
            }
        }
    }

    void EnemyMovement()
    {
        if(!isMoving)
        { 
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            enemyTargetTile.targetTile = true;
        }
        else
        {
            gameObject.tag = "ActivePlayer";
            moveSelect = true;
            CharacterMove();
        }
    }

    void ShouldEndTurn()
    {
        if(currentActionPoints <= 0)
        {
            BasicAttack();
            turnManager.FinishTurn();
            gameObject.tag = "Enemy";
            turnManager.enemyTurn = false;
            turnManager.playerTurn = true;
            target = null;
        }

        if(moveActionsThisTurn > 0)
        {
            BasicAttack();
            turnManager.FinishTurn();
            gameObject.tag = "Enemy";
            turnManager.enemyTurn = false;
            turnManager.playerTurn = true;
            target = null;
        }
    }
}
