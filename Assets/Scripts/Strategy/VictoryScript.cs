using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    public bool victoryAchieved = false;
    public GameObject objectivePanel;
    public StrategySceneTrigger readForVictory;
    public string readFromSceneTrigger;

    public TurnManager turnManager;

    public string targetName;
    public GameObject targetEnemy;

    public float targetTurn;

    public enum VictoryConditions
    {
        targetDefeated,
        enemiesCleared,
        playersEscape,
        playersSurvived
    }

    public VictoryConditions currentVictoryCondition;

    void Update()
    {
        if (readFromSceneTrigger == null)
        {
            readFromSceneTrigger = readForVictory.victoryCondition;
        }

        if(readFromSceneTrigger == "Target Defeated")
        {
            currentVictoryCondition = VictoryConditions.targetDefeated;
        }
        else if(readFromSceneTrigger == "Enemies Cleared")
        {
            currentVictoryCondition = VictoryConditions.enemiesCleared;
        }
        else if(readFromSceneTrigger == "Players Escape")
        {
            currentVictoryCondition = VictoryConditions.playersEscape;
        }
        else if(readFromSceneTrigger == "Players Survived")
        {
            currentVictoryCondition = VictoryConditions.playersSurvived;
        }

        switch(currentVictoryCondition)
        {
            case VictoryConditions.targetDefeated:

                //Gather enemy list
                List<GameObject> potentialTargetList = new List<GameObject>();
                GameObject[] livingTargets = GameObject.FindGameObjectsWithTag("Enemy");
                
                foreach(GameObject potential in livingTargets)
                {
                    if(!potentialTargetList.Contains(potential))
                    {
                        potentialTargetList.Add(potential);
                    }
                }

                //Determine which enemy is the target
                targetEnemy = GameObject.Find(targetName);

                foreach(GameObject enemy in potentialTargetList)
                {
                    EnemyController enemyController = enemy.GetComponent<EnemyController>();

                    if(enemyController.currentHealth <= 0)
                    {
                        potentialTargetList.Remove(enemy);
                    }
                }

                if(turnManager.turnCount > 2 && !potentialTargetList.Contains(targetEnemy))
                {
                    victoryAchieved = true;
                }
                else
                {
                    victoryAchieved = false;
                }

                break;

            case VictoryConditions.enemiesCleared:

                //Gather enemy list
                List<GameObject> livingEnemyList = new List<GameObject>();
                GameObject[] findEnemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject foundEnemy in findEnemies)
                {
                    if (!livingEnemyList.Contains(foundEnemy))
                    {
                        livingEnemyList.Add(foundEnemy);
                    }
                }

                //Check if enemy is alive
                foreach(GameObject enemy in livingEnemyList)
                {
                    EnemyController enemyController = enemy.GetComponent<EnemyController>();

                    if(enemyController.currentHealth <= 0)
                    {
                        livingEnemyList.Remove(enemy);
                    }
                }

                if(turnManager.turnCount > 2 && livingEnemyList.Count > 1)
                {
                    victoryAchieved = true;
                }
                else
                {
                    victoryAchieved = false;
                }

                break;

            case VictoryConditions.playersEscape:

                List<GameObject> playersToEscape = new List<GameObject>();
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                foreach(GameObject player in players)
                {
                    if(!playersToEscape.Contains(player))
                    {
                        playersToEscape.Add(player);
                    }
                }

                foreach(GameObject player in playersToEscape)
                {
                    PlayerCharacter playerController = player.GetComponent<PlayerCharacter>();

                    if(playerController.hasEscaped)
                    {
                        playersToEscape.Remove(player);
                    }
                }

                if(playersToEscape.Count < 1 && turnManager.turnCount > 2)
                {
                    victoryAchieved = true;
                }
                else
                {
                    victoryAchieved = false;
                }

                break;

            case VictoryConditions.playersSurvived:

                if(turnManager.turnCount >= targetTurn)
                {
                    victoryAchieved = true;
                }
                else
                {
                    victoryAchieved = false;
                }

                break;
        }
    }

    public void ConfirmButton()
    {
        objectivePanel.SetActive(false);
    }
}
