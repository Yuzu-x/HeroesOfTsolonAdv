using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<CharacterController>> units = new Dictionary<string, List<CharacterController>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<CharacterController> turnTeam = new Queue<CharacterController>();

    public float turnCount = 0f;
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
        List<CharacterController> teamList = units[turnKey.Peek()];

        foreach(CharacterController unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }
        StartTurn();
    }

    void StartTurn()
    {
        foreach(CharacterController character in turnTeam)
        {
            character.currentActionPoints = character.restoredActionPoints;
            character.moveActionsThisTurn = 0f;
            character.restoredActionPoints = character.maximumActionPoints;
        }

        if(turnTeam.Count > 0)
        {
            turnTeam.Peek().TurnBegin();
        }
    }

    void TurnCountUpdate()
    {
        turnCountText.text = "Turn " + turnCount;
    }

    public void FinishTurn()
    {
        CharacterController unit = turnTeam.Dequeue();
        unit.TurnEnd();

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

    public void AddUnit(CharacterController unit)
    {
        List<CharacterController> list;

        if(!units.ContainsKey(unit.tag))
        {
            list = new List<CharacterController>();
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
}
