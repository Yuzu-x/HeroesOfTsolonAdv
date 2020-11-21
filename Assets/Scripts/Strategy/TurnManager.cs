using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<GameObject>> units = new Dictionary<string, List<GameObject>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<GameObject> turnTeam = new Queue<GameObject>();

    public float turnCount = 0f;
    public bool turnCounted = false;
    public Text turnCountText;

    public float coinFlip = 3;
    public bool currentlyTurnZero = true;

    public bool playerTurn = false;
    public bool enemyTurn = false;

    void Start()
    {
        TurnCountUpdate();
    }

    void Update()
    {
        if (!currentlyTurnZero)
        {
            if (turnTeam.Count == 0)
            {
                InitTeamTurnQueue();
            }
            TurnCountUpdate();
        }
    }

    void InitTeamTurnQueue()
    {
        List<GameObject> teamList = units[turnKey.Peek()];

        foreach(GameObject unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }
        StartTurn();
    }

    void StartTurn()
    {
        foreach(GameObject character in turnTeam)
        {
            PlayerCharacter playerController = character.GetComponent<PlayerCharacter>();
            EnemyController enemyController = character.GetComponent<EnemyController>();

            if (playerController != null)
            {
                playerController.currentActionPoints = playerController.restoredActionPoints;
                playerController.moveActionsThisTurn = 0f;
                playerController.restoredActionPoints = playerController.maximumActionPoints;
                playerController.InitCheck();
            }
            else if (enemyController != null)
            {
                enemyController.currentActionPoints = enemyController.restoredActionPoints;
                enemyController.moveActionsThisTurn = 0f;
                enemyController.restoredActionPoints = enemyController.maximumActionPoints;
            }
        }

        if(turnTeam.Count > 0)
        {
           GameObject checkQueue = turnTeam.Peek();

            if(checkQueue.GetComponent<PlayerCharacter>())
            {
                AddUnit(checkQueue);
            }
            else if (checkQueue.GetComponent<EnemyController>())
            {
                AddUnit(checkQueue);
            }
        }
    }

    void TurnCountUpdate()
    {
        turnCountText.text = "Turn " + turnCount;
    }

    public void FinishTurn()
    {
        GameObject unit = turnTeam.Dequeue();

        PlayerCharacter unitPlayer = unit.GetComponent<PlayerCharacter>();
        EnemyController unitEnemy = unit.GetComponent<EnemyController>();

        if(unitPlayer)
        {
            unitPlayer.TurnEnd();
        }
        else if(unitEnemy)
        {
            unitEnemy.TurnEnd();
        }

        if(turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    public void AddUnit(GameObject unit)
    {
        List<GameObject> list;

        if(!units.ContainsKey(unit.tag))
        {
            list = new List<GameObject>();
            units[unit.tag] = list;

            if(!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }
        list.Add(unit);
    }

    public void FirstTurnDetermination()
    {
        coinFlip = Random.Range(1, 3);

        if (coinFlip == 1)
        {
            playerTurn = true;
            coinFlip = 3;
        }
        else if (coinFlip == 2)
        {
            enemyTurn = true;
            coinFlip = 3;
        }

        currentlyTurnZero = false;
        turnCount += 1;
    }
}
