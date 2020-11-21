using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpellFunction : MonoBehaviour
{
    public SpellFramework spellFramework;
    public bool isCasting = false;
    public bool stopDamaging = false;
    public bool repeatCast = false;
    public float repeatTotal;

    public TileController tileAimed;
    public TileController tileTargeted;
    public List<TileController> tileSelectable = new List<TileController>();
    public List<TileController> affectedTiles = new List<TileController>();

    GameObject[] tiles;
    TileController originTile;

    public PlayerCharacter playerController;
    public KeyCode confirmTargetKey = KeyCode.C;
    public bool targetConfirmed = false;

    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    void Update()
    {
        if (tileTargeted != null)
        {
            switch (spellFramework.targetStyle)
            {
                case SpellFramework.SpellTargetStyle.SELECTED:
                    TargetedSpell();
                    break;

                case SpellFramework.SpellTargetStyle.LINE:
                    LineSpell();
                    break;

                case SpellFramework.SpellTargetStyle.CONE:
                    ConeSpell();
                    break;

                case SpellFramework.SpellTargetStyle.BURST:
                    BurstSpell();
                    break;

                case SpellFramework.SpellTargetStyle.BLAST:
                    BlastSpell();
                    break;

                case SpellFramework.SpellTargetStyle.NONE:

                    break;
            }
        }


        GameObject activePlayer = GameObject.FindGameObjectWithTag("ActivePlayer");

        if (!stopDamaging && playerController == null || playerController != activePlayer)
        {
            if (activePlayer != null)
            {
                playerController = activePlayer.GetComponent<PlayerCharacter>();
            }
        }
    }

    void TargetedSpell()
    {
        Queue<TileController> targetTileCheck = new Queue<TileController>();
        targetTileCheck.Enqueue(tileTargeted);
        tileTargeted.visitedTile = true;

        while(targetTileCheck.Count > 0)
        {
            TileController targetCheck = targetTileCheck.Dequeue();
            if(!affectedTiles.Contains(targetCheck))
            {
                affectedTiles.Add(targetCheck);
            }
            tileTargeted.affectedTile = true;
        }
    }

    void LineSpell()
    {
        Queue<TileController> lineTileCheck = new Queue<TileController>();
        lineTileCheck.Enqueue(originTile);
        originTile.visitedTile = true;

        Queue<TileController> forwardLineCheck = new Queue<TileController>();
        Queue<TileController> backwardLineCheck = new Queue<TileController>();
        Queue<TileController> leftwardLineCheck = new Queue<TileController>();
        Queue<TileController> rightwardLineCheck = new Queue<TileController>();

        while(lineTileCheck.Count > 0)
        {
            TileController lineCheck = lineTileCheck.Dequeue();

            if(lineCheck != originTile && !affectedTiles.Contains(lineCheck))
            {
                affectedTiles.Add(lineCheck);
                lineCheck.affectedTile = true;

                if(forwardLineCheck.Contains(lineCheck) && lineCheck.distance < spellFramework.spellRange)
                {
                    RaycastHit forwardContinue;

                    if(Physics.Raycast(lineCheck.transform.position, Vector3.forward, out forwardContinue, 1))
                    {
                        TileController nextInLine = forwardContinue.collider.GetComponent<TileController>();
                        nextInLine.visitedTile = true;
                        nextInLine.distance = 1 + lineCheck.distance;
                        forwardLineCheck.Enqueue(nextInLine);
                        lineTileCheck.Enqueue(nextInLine);
                    }
                }
                else if(backwardLineCheck.Contains(lineCheck) && lineCheck.distance < spellFramework.spellRange)
                {
                    RaycastHit backwardContinue;
                    
                    if(Physics.Raycast(lineCheck.transform.position, -Vector3.forward, out backwardContinue, 1))
                    {
                        TileController nextInLine = backwardContinue.collider.GetComponent<TileController>();
                        nextInLine.visitedTile = true;
                        nextInLine.distance = 1 + lineCheck.distance;
                        backwardLineCheck.Enqueue(nextInLine);
                        lineTileCheck.Enqueue(nextInLine);
                    }
                }
            }

            if(lineCheck == originTile)
            {
                RaycastHit forwardLine;
                RaycastHit backwardLine;
                RaycastHit leftwardLine;
                RaycastHit rightwardLine;

                if(Physics.Raycast(lineCheck.transform.position, Vector3.forward, out forwardLine, 1))
                {
                    TileController forwardCheck = forwardLine.collider.GetComponent<TileController>();
                    forwardCheck.visitedTile = true;
                    forwardCheck.distance = 1 + originTile.distance;
                    forwardLineCheck.Enqueue(forwardCheck);
                }

                if(Physics.Raycast(lineCheck.transform.position, -Vector3.forward, out backwardLine, 1))
                {
                    TileController backwardCheck = backwardLine.collider.GetComponent<TileController>();
                    backwardCheck.visitedTile = true;
                    backwardCheck.distance = 1 + originTile.distance;
                    backwardLineCheck.Enqueue(backwardCheck);
                }

                if(Physics.Raycast(lineCheck.transform.position, Vector3.right, out rightwardLine, 1))
                {
                    TileController rightwardCheck = rightwardLine.collider.GetComponent<TileController>();
                    rightwardCheck.visitedTile = true;
                    rightwardCheck.distance = 1 + originTile.distance;
                    rightwardLineCheck.Enqueue(rightwardCheck);
                }

                if(Physics.Raycast(lineCheck.transform.position, -Vector3.right, out leftwardLine, 1))
                {
                    TileController leftwardCheck = leftwardLine.collider.GetComponent<TileController>();
                    leftwardCheck.visitedTile = true;
                    leftwardCheck.distance = 1 + originTile.distance;
                    leftwardLineCheck.Enqueue(leftwardCheck);
                }
            }
        }
    }

    void ConeSpell()
    {

    }

    void BurstSpell()
    {
        TileAdjacency(null);

        Queue<TileController> burstTileCheck = new Queue<TileController>();
        burstTileCheck.Enqueue(originTile);
        originTile.visitedTile = true;

        while(burstTileCheck.Count > 0)
        {
            TileController burstCheck = burstTileCheck.Dequeue();
            if(!affectedTiles.Contains(burstCheck))
            {
                affectedTiles.Add(burstCheck);
            }
            burstCheck.affectedTile = true;

            if(burstCheck.distance < spellFramework.burstRadius)
            {
                foreach(TileController uncheckedBurst in burstCheck.adjacentTileSpellList)
                {
                    if(!uncheckedBurst.visitedTile)
                    {
                        uncheckedBurst.parentTile = burstCheck;
                        uncheckedBurst.visitedTile = true;
                        uncheckedBurst.distance = 1 + burstCheck.distance;
                        burstTileCheck.Enqueue(uncheckedBurst);
                    }
                }
            }
        }
    }

    void BlastSpell()
    {
        TileAdjacency(null);

        Queue<TileController> blastTileCheck = new Queue<TileController>();
        blastTileCheck.Enqueue(tileTargeted);
        tileTargeted.visitedTile = true;

        while (blastTileCheck.Count > 0)
        {
            TileController blastCheck = blastTileCheck.Dequeue();
            if (!affectedTiles.Contains(blastCheck))
            {
                affectedTiles.Add(blastCheck);
            }
            blastCheck.affectedTile = true;

            if (blastCheck.distance < spellFramework.burstRadius)
            {
                if (spellFramework.spellType != SpellFramework.SpellType.SUMMON)
                {
                    foreach (TileController uncheckedBlast in blastCheck.adjacentTileSpellList)
                    {
                        if (!uncheckedBlast.visitedTile)
                        {
                            uncheckedBlast.parentTile = blastCheck;
                            uncheckedBlast.visitedTile = true;
                            uncheckedBlast.distance = 1 + blastCheck.distance;
                            blastTileCheck.Enqueue(uncheckedBlast);
                        }
                    }
                }
                else
                {
                    foreach (TileController uncheckedBlast in blastCheck.adjacentTileList)
                    {
                        if (!uncheckedBlast.visitedTile)
                        {
                            uncheckedBlast.parentTile = blastCheck;
                            uncheckedBlast.visitedTile = true;
                            uncheckedBlast.distance = 1 + blastCheck.distance;
                            blastTileCheck.Enqueue(uncheckedBlast);
                        }
                    }
                }
            }
        }
    }

    public void ResolveSpell()
    {
        List <PlayerCharacter> affectedPlayers = new List<PlayerCharacter>();
        List<EnemyController> affectedEnemies = new List<EnemyController>();

        if (Input.GetKeyUp(confirmTargetKey))
        {
            targetConfirmed = true;

            if(spellFramework.spellType == SpellFramework.SpellType.SUMMON)
            {
                repeatTotal = spellFramework.spellSummonQuantity;
            }
        }

        if(targetConfirmed)
        { 
            switch (spellFramework.spellType)
            {
                case SpellFramework.SpellType.DAMAGE:

                    foreach (TileController affected in affectedTiles)
                    {
                        RaycastHit checkForOccupier;

                        if (Physics.Raycast(affected.transform.position, Vector3.up, out checkForOccupier, 1))
                        {
                            if (checkForOccupier.collider.GetComponent<PlayerCharacter>() != null)
                            {
                                affectedPlayers.Add(checkForOccupier.collider.GetComponent<PlayerCharacter>());
                            }
                            else if (checkForOccupier.collider.GetComponent<EnemyController>() != null)
                            {
                                Debug.Log("Found " + checkForOccupier.collider.gameObject);
                                affectedEnemies.Add(checkForOccupier.collider.GetComponent<EnemyController>());
                            }
                        }

                        foreach (PlayerCharacter player in affectedPlayers)
                        {
                            if (spellFramework.targetStyle == SpellFramework.SpellTargetStyle.BURST && player == playerController)
                            {

                            }
                            else
                            {
                                player.TakeDamage(spellFramework.spellDamage);
                                player.stopTakingDamage = true;
                            }
                        }

                        foreach (EnemyController enemy in affectedEnemies)
                        {
                            enemy.TakeDamage(spellFramework.spellDamage);
                            enemy.stopTakingDamage = true;
                        }
                    }

                    break;

                case SpellFramework.SpellType.HEALING:

                    foreach(TileController tile in affectedTiles)
                    {
                        RaycastHit checkForOccupier;

                        if(Physics.Raycast(tile.transform.position, Vector3.up, out checkForOccupier, 1))
                        {
                            affectedPlayers.Add(checkForOccupier.collider.GetComponent<PlayerCharacter>());
                        }

                        foreach(PlayerCharacter player in affectedPlayers)
                        {
                            player.Healed(spellFramework.spellHealing);
                            player.stopTakingDamage = true;
                        }
                    }

                    break;

                case SpellFramework.SpellType.BOON:

                    foreach(TileController tile in affectedTiles)
                    {
                        RaycastHit checkForOccupier;

                        if (Physics.Raycast(tile.transform.position, Vector3.up, out checkForOccupier, 1))
                        {
                            affectedPlayers.Add(checkForOccupier.collider.GetComponent<PlayerCharacter>());
                        }

                        foreach(PlayerCharacter player in affectedPlayers)
                        {
                            Debug.Log("Apply Boon");
                        }
                    }

                    break;

                case SpellFramework.SpellType.PERSONAL:



                    break;

                case SpellFramework.SpellType.SUMMON:

                    if(repeatTotal > 1)
                    {
                        repeatCast = true;
                    }
                    else if(repeatTotal == 0)
                    {
                        repeatCast = false;
                    }
                    

                    foreach (TileController tile in affectedTiles)
                    {
                        RaycastHit checkForOccupier;

                        if(!Physics.Raycast(tile.transform.position, Vector3.up, out checkForOccupier, 1))
                        {
                            if (repeatTotal > 0 && affectedTiles.Count > 0)
                            {
                                var randomSelection = new System.Random();
                                int selectedTile = randomSelection.Next(affectedTiles.Count);
                                TileController instantiateTile = affectedTiles[selectedTile];

                                GameObject summoned = Instantiate(spellFramework.summonObject);
                                summoned.transform.SetParent(instantiateTile.transform);
                                summoned.transform.position = new Vector3(instantiateTile.transform.position.x, instantiateTile.transform.position.y + 1, instantiateTile.transform.position.z);
                                summoned.transform.rotation = instantiateTile.transform.rotation;
                                summoned.transform.SetParent(null);

                                repeatTotal -= 1;
                                instantiateTile.Reset();
                                affectedTiles.Remove(instantiateTile);
                            }           
                        }
                    }

                    break;
            }
            if (!repeatCast)
            {
                FinishSpell();
                stopDamaging = true;
            }
            else
            {
                ResolveSpell();
            }
        }
    }


    public void CastButton()
    {
        playerController.currentState = CharacterController.TurnState.CASTING;
        isCasting = true;
        stopDamaging = false;
    }

    public void CastSelect()
    {
        if(playerController.isActive && isCasting)
        {
            TargetableTiles();
            SpellAiming();

        }
        else
        {
            ResolveSpell();
        }
    }

    void SpellAiming()
    {
        Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit spellAimHit;

        if(Physics.Raycast(aimRay, out spellAimHit))
        {
            if(spellAimHit.collider.tag == "Tile")
            {
                TileController spellTile = spellAimHit.collider.GetComponent<TileController>();

                if(spellTile.selectableTile)
                {
                    spellTile.GetComponent<Renderer>().material.color = Color.cyan;

                    if (Input.GetMouseButtonUp(0))
                    {
                        spellTile.aimLockedTile = true;
                        tileTargeted = spellTile;
                        isCasting = false;
                        playerController.dealtDamage = true;
                    }
                }
            }
        }
    }

    public void OriginTile()
    {
        originTile = TargetTile(playerController.gameObject);
        originTile.currentTile = true;
    }

    public TileController TargetTile(GameObject actioned)
    {
        RaycastHit hitTile;
        TileController tile = null; 
        if(Physics.Raycast(actioned.transform.position, -Vector3.up, out hitTile, 1))
        {
            tile = hitTile.collider.GetComponent<TileController>();
        }
        return tile;
    }

    protected void TileAdjacency(TileController target)
    {
        foreach(GameObject tile in tiles)
        {
            TileController checkT = tile.GetComponent<TileController>();
            checkT.FindNeighbours(3, target);
        }
    }

    public void TargetableTiles()
    {
        TileAdjacency(null);
        OriginTile();

        Queue<TileController> tileQueue = new Queue<TileController>();
        tileQueue.Enqueue(originTile);
        originTile.visitedTile = true;

        while(tileQueue.Count > 0)
        {
            TileController examineTile = tileQueue.Dequeue();

            if (!tileSelectable.Contains(examineTile))
            {
                tileSelectable.Add(examineTile);
            }

            examineTile.selectableTile = true;

            if(examineTile.distance < spellFramework.spellRange)
            {
                foreach(TileController tileObj in examineTile.adjacentTileSpellList)
                {
                    if(!tileObj.visitedTile)
                    {
                        tileObj.parentTile = examineTile;
                        tileObj.visitedTile = true;
                        tileObj.distance = 1 + examineTile.distance;
                        tileQueue.Enqueue(tileObj);
                    }
                }
            }
        }
    }

    public void FinishSpell()
    {
        playerController.SpendActionPoints(spellFramework.actionPointCost);
        targetConfirmed = false;
        repeatTotal = 0;

        if(spellFramework.fatigueCost != 0)
        {
            playerController.Fatigued(spellFramework.fatigueCost);
        }

        CharacterClassClass coolDown = playerController.GetComponent<CharacterClassClass>();
        coolDown.BeginCooldown(spellFramework.spellName, spellFramework.maximumCoolDown);

        foreach(TileController tile in affectedTiles)
        {
            tile.Reset();
        }

        foreach(GameObject tile in tiles)
        {
            TileController tileContr = tile.GetComponent<TileController>();

            if(tileContr.selectableTile)
            {
                tileContr.selectableTile = false;
            }
        }

        tileTargeted = null;
        originTile = null;
        playerController.currentState = CharacterController.TurnState.WAITING;
        isCasting = false;
        if (playerController != null)
        {
            playerController = null;
        }
        affectedTiles.Clear();

        Debug.Log("playerController should be null");

        foreach (GameObject check in GameObject.FindGameObjectsWithTag("Player"))
        {
            PlayerCharacter playerCheck = check.GetComponent<PlayerCharacter>();

            if(playerCheck.stopTakingDamage)
            {
                playerCheck.stopTakingDamage = false;
            }
        }

        foreach(GameObject checkAc in GameObject.FindGameObjectsWithTag("ActivePlayer"))
        {
            PlayerCharacter activeCheck = checkAc.GetComponent<PlayerCharacter>();
            EnemyController activeEn = checkAc.GetComponent<EnemyController>();

            if(activeCheck != null && activeCheck.stopTakingDamage)
            {
                activeCheck.stopTakingDamage = false;
            }
            else if(activeEn != null && activeEn.stopTakingDamage)
            {
                activeEn.stopTakingDamage = false;
            }
        }

        foreach(GameObject checkEn in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyController enemyCheck = checkEn.GetComponent<EnemyController>();

            if(enemyCheck.stopTakingDamage)
            {
                enemyCheck.stopTakingDamage = false;
            }
        }
    }
}
