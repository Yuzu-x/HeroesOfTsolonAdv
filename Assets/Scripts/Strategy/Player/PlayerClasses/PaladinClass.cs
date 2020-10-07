using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaladinClass : PlayerCharacter
{
    public PlayerCharacter playerCharacter;
    public CharacterClassClass cooldownManager;
    GameObject[] checkingDealt;
    string[] allyTags = { "Player", "ActivePlayer" };

    public Button blazingButton;
    public string blazingDescription;
    public Button flashButton;
    public string flashDescription;
    public Button perfectButton;
    public string perfectDescription;
    public Button humblingButton;
    public string humblingDescription;

    public float perfectTauntRadius = 6;
    public float perfStartTurn;

    void Start()
    {
        if(playerCharacter == null)
        {
            playerCharacter = this.GetComponent<PlayerCharacter>();
        }

        if (cooldownManager == null)
        {
            cooldownManager = this.GetComponent<CharacterClassClass>();
        }
    }

    void Update()
    {
        if (takeDamage && !isActive)
        {
            ExceptionalForgiveness();
        }

        if(tag == "ActivePlayer")
        {
            blazingButton.onClick.AddListener(BlazingStrike);
            Text blazButtonText = blazingButton.GetComponentInChildren<Text>();
            blazButtonText.text = "Blazing Strike";
            flashButton.onClick.AddListener(Flash);
            Text flashButtonText = flashButton.GetComponentInChildren<Text>();
            flashButtonText.text = "Flash";
            perfectButton.onClick.AddListener(PerfectClarity);
            Text perfButtonText = perfectButton.GetComponentInChildren<Text>();
            perfButtonText.text = "Perfect Clarity";
            humblingButton.onClick.AddListener(HumblingShout);
            Text humblButtonText = humblingButton.GetComponentInChildren<Text>();
            humblButtonText.text = "Humbling Shout";
        }
    }

    void ExceptionalForgiveness()
    {
        foreach (string tag in allyTags)
        {
            checkingDealt = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject check in checkingDealt)
            {
                PlayerCharacter playerChar = check.GetComponent<PlayerCharacter>();

                if (playerChar.dealtDamage)
                {
                    this.receivedDamage /= 2f;
                }
            }
        }
    }

    void BlazingStrike()
    {
        if (!cooldownManager.spellCooling.Contains("BlazingStrike") && playerCharacter.currentState == TurnState.WAITING && playerCharacter.currentActionPoints >= 1)
        {
            playerCharacter.SpendActionPoints(1);
            playerCharacter.currentState = TurnState.CASTING;
            cooldownManager.BeginCooldown("BlazingStrike", 1);
            playerCharacter.basicDamage += 40;
            playerCharacter.BasicAttack();
            playerCharacter.basicDamage -= 40;
        }
    }

    void Flash()
    {
        if (!cooldownManager.spellCooling.Contains("Flash") && playerCharacter.currentState == TurnState.WAITING && playerCharacter.currentActionPoints >= 2)
        {
            playerCharacter.SpendActionPoints(2);
            playerCharacter.Healed(playerCharacter.maximumHealth / 5);
            GetInitialTile();

            RaycastHit targetSearch;
            RaycastHit enemyLocated;
            TileController tileOccupied = null;
            
            if(Physics.Raycast(initialTile.transform.position, this.transform.forward, out targetSearch, 1) || Physics.Raycast(initialTile.transform.position, this.transform.right, out targetSearch, 1) || Physics.Raycast(initialTile.transform.position, -this.transform.forward, out targetSearch, 1) || Physics.Raycast(initialTile.transform.position, -this.transform.right, out targetSearch, 1))
            {
                tileOccupied = targetSearch.collider.GetComponent<TileController>();

                if(!tileOccupied.selectableTile)
                {
                    if(Physics.Raycast(transform.position, transform.forward, out enemyLocated, 1) || Physics.Raycast(transform.position, transform.right, out enemyLocated, 1) || Physics.Raycast(transform.position, -transform.forward, out enemyLocated, 1) || Physics.Raycast(transform.position, -transform.right, out enemyLocated, 1))
                    {
                        if(enemyLocated.collider.tag == "Enemy")
                        {
                            EnemyController enCont = enemyLocated.collider.GetComponent<EnemyController>();
                            enCont.restoredActionPoints -= 1;
                        }
                    }
                }
            }

            cooldownManager.BeginCooldown("Flash", 3);
        }
    }

    void PerfectClarity()
    {
        if (!cooldownManager.spellCooling.Contains("PerfectClarity") && playerCharacter.currentState == TurnState.WAITING && playerCharacter.currentActionPoints >= 2 && playerCharacter.fatigue < 1)
        {
            playerCharacter.SpendActionPoints(2);
            playerCharacter.Fatigued(2);
            perfStartTurn = turnManager.turnCount;

            if(perfStartTurn == turnManager.turnCount && !myTurn)
            {
                playerCharacter.hasTaunted = true;
                playerCharacter.isGuarding = true;
                guardMultiplier = 0;
            }
            else if(perfStartTurn < turnManager.turnCount)
            {
                playerCharacter.hasTaunted = false;
                playerCharacter.isGuarding = false;
            }

            Collider[] perfectRadiusCheck = Physics.OverlapSphere(transform.position, perfectTauntRadius);

            foreach(Collider check in perfectRadiusCheck)
            {
                if(check.tag == "Enemy")
                {
                    EnemyController tauntedEnemy = check.GetComponent<EnemyController>();

                    tauntedEnemy.target = this.gameObject;
                }
            }

            cooldownManager.BeginCooldown("PerfectClarity", 4);
        }
    }

    void HumblingShout()
    {
        if(!cooldownManager.spellCooling.Contains("HumblingShout") && playerCharacter.currentState == TurnState.WAITING && playerCharacter.currentActionPoints >= 1 && playerCharacter.fatigue < 1)
        {
            playerCharacter.SpendActionPoints(1);
            playerCharacter.Fatigued(1);



            cooldownManager.BeginCooldown("HumblingShout", 3);
        }
    }
}

