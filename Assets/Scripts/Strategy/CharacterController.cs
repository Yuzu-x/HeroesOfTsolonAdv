using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    //Determine the turn
    public bool myTurn = false;
    public TurnManager turnManager;

    //Find selectable tiles
    public List<TileController> selectableTiles = new List<TileController>();
    GameObject[] tiles;

    //Orientate self
    Stack<TileController> path = new Stack<TileController>();
    public TileController initialTile;

    //Movement variables
    public bool isMoving = false;
    public bool isActive = false;
    public bool moveSelect = false;
    public bool runSelect = false;
    public float moveActionsThisTurn = 0;

    //Travel vectors
    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    //Accessible tile
    float halfHeight = 0;

    //Travelling Y axis
    public bool fallingDown = false;
    public bool jumpingUp = false;
    public bool moveToEdge = false;
    Vector3 jumpTarget;

    //Enemy target
    public TileController enemyTargetTile;

    //Character Attributes
    public float moveRange;
    public float runRange;
    public float jumpHeight = 3;
    public float movementSpeed = 2;
    public float jumpVelocity = 4.5f;
    public float maximumActionPoints = 4f;
    public float currentActionPoints;
    public Image actionPointImage;
    public float maximumHealth;
    public float currentHealth;
    public Image healthImage;
    public float fatigue = 0;

    public enum TurnState
    {
        MOVING,
        CASTING,
        LONGCASTING,
        RUNNING,
        WAITING,
        DEAD
    }

    public TurnState currentState;

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        halfHeight = GetComponent<Collider>().bounds.extents.y;

        turnManager.AddUnit(this);
    }

    void Update()
    {
        actionPointImage.fillAmount = currentActionPoints / maximumActionPoints;
        healthImage.fillAmount = currentHealth / maximumHealth;
    }

    //Movement Handling
    public void GetInitialTile()
    {
        initialTile = GetTargetTile(gameObject);
        initialTile.currentTile = true;
    }

    public TileController GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        TileController tile = null;
        if(Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<TileController>();
        }
        return tile;
    }

    public void ComputeAdjacencyLists(float jumpHeight, TileController target)
    {
        foreach(GameObject tile in tiles)
        {
            TileController t = tile.GetComponent<TileController>();
            t.FindNeighbours(jumpHeight, target);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists(jumpHeight, null);
        GetInitialTile();

        Queue<TileController> processQueue = new Queue<TileController>();

        processQueue.Enqueue(initialTile);
        initialTile.visitedTile = true;

        while (processQueue.Count > 0)
        {
            TileController tile = processQueue.Dequeue();
            selectableTiles.Add(tile);
            tile.selectableTile = true;

            if(tile.distance < moveRange)
            {
                foreach(TileController tileObject in tile.adjacentTileList)
                {
                    if(!tileObject.visitedTile)
                    {
                        tileObject.parentTile = tile;
                        tileObject.visitedTile = true;
                        tileObject.distance = 1 + tile.distance;
                        processQueue.Enqueue(tileObject);
                    }
                }
            }
        }
    }

    public void MoveToTile(TileController tile)
    {
        path.Clear();
        tile.targetTile = true;
        isMoving = true;
        TileController nextTile = tile;

        while(nextTile != null)
        {
            path.Push(nextTile);
            nextTile = nextTile.parentTile;
        }
    }

    public void CharacterMove()
    {
        if(path.Count > 0)
        {
            TileController tile = path.Peek();
            Vector3 target = tile.transform.position;
            target.y += halfHeight + tile.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                bool shouldJump = transform.position.y != target.y;

                if(shouldJump)
                {
                    MoveVertically(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            isMoving = false;
            MoveEnd();
        }
    }

    void MoveEnd()
    {
        SpendActionPoints(1);

        if(moveSelect)
        {
            moveActionsThisTurn += 1;
        }
        else if(runSelect)
        {
            Fatigued(1);
        }
        moveSelect = false;
        runSelect = false;
        currentState = TurnState.WAITING;
    }

    protected void RemoveSelectableTiles()
    {
        if(initialTile != null)
        {
            initialTile.currentTile = false;
            initialTile = null;
        }
        
        foreach(TileController tile in selectableTiles)
        {
            tile.Reset();
        }

        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * movementSpeed;
    }

    void MoveVertically(Vector3 target)
    {
        if(fallingDown)
        {
            FallDown(target);
        }
        else if(jumpingUp)
        {
            JumpUp(target);
        }
        else if(moveToEdge)
        {
            ApproachEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target)
    {
        float targetY = target.y;
        target.y = transform.position.y;
        CalculateHeading(target);

        if(transform.position.y > targetY)
        {
            fallingDown = false;
            jumpingUp = false;
            moveToEdge = true;
            jumpTarget = transform.position + (target - transform.position) / 2f;
        }
        else
        {
            fallingDown = false;
            jumpingUp = true;
            moveToEdge = false;
            velocity = heading * movementSpeed / 3f;

            float difference = targetY - transform.position.y;
            velocity.y = jumpVelocity * (0.5f + difference / 2f);
        }
    }

    void FallDown(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if(transform.position.y <= target.y)
        {
            fallingDown = false;
            Vector3 pos = transform.position;
            pos.y = target.y;
            transform.position = pos;
            velocity = new Vector3();
        }
    }

    void JumpUp(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if(transform.position.y > target.y)
        {
            jumpingUp = false;
            fallingDown = true;
        }
    }

    void ApproachEdge()
    {
        if(Vector3.Distance(transform.position, jumpTarget) >= 0.05f)
        {
            SetHorizontalVelocity();
        }
        else
        {
            moveToEdge = false;
            fallingDown = true;
            velocity /= 3f;
            velocity.y = 1.5f;
        }
    }

    protected TileController FindLowestF(List<TileController> list)
    {
        TileController lowest = list[0];

        foreach(TileController tile in list)
        {
            if(tile.f < lowest.f)
            {
                lowest = tile;
            }
        }
        list.Remove(lowest);
        return lowest;
    }

    protected TileController FindEndTile(TileController tile)
    {
        Stack<TileController> tempPath = new Stack<TileController>();
        TileController nextTile = tile.parentTile;

        while(nextTile != null)
        {
            tempPath.Push(nextTile);
            nextTile = nextTile.parentTile;
        }

        if(tempPath.Count <= moveRange)
        {
            return tile.parentTile;
        }

        TileController endTile = null;

        for (int i = 0; i <= moveRange; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;
    }

    protected void FindPath(TileController target)
    {
        ComputeAdjacencyLists(jumpHeight, target);
        GetInitialTile();

        List<TileController> openList = new List<TileController>();
        List<TileController> closedList = new List<TileController>();

        openList.Add(initialTile);
        initialTile.h = Vector3.Distance(initialTile.transform.position, target.transform.position);
        initialTile.f = initialTile.h;

        while(openList.Count > 0)
        {
            TileController tile = FindLowestF(openList);
            closedList.Add(tile);

            if(tile == target)
            {
                enemyTargetTile = FindEndTile(tile);
                MoveToTile(enemyTargetTile);
                return;
            }

            foreach(TileController tileObject in tile.adjacentTileList)
            {
                if(closedList.Contains(tileObject))
                {

                }
                else if(openList.Contains(tileObject))
                {
                    float tempG = tile.g + Vector3.Distance(tileObject.transform.position, tile.transform.position);

                    if(tempG < tileObject.g)
                    {
                        tileObject.parentTile = tile;
                        tileObject.g = tempG;
                        tileObject.f = tileObject.g + tileObject.h;
                    }
                }
                else
                {
                    tileObject.parentTile = tile;
                    tileObject.g = tile.g + Vector3.Distance(tileObject.transform.position, tile.transform.position);
                    tileObject.h = Vector3.Distance(tileObject.transform.position, target.transform.position);
                    tileObject.f = tileObject.g + tileObject.h;
                    openList.Add(tileObject);
                }
            }
        }
    }

    //Turn Management

    public void TurnBegin()
    {
        myTurn = true;

        if(fatigue > 0)
        {
            fatigue -= 1;
        }
    }

    public void TurnEnd()
    {
        myTurn = false;
    }

    //Character Resources

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if(currentHealth <= 0)
        {
            currentState = TurnState.DEAD;
        }
    }

    public void SpendActionPoints(float pointsSpent)
    {
        currentActionPoints -= pointsSpent;
    }

    public void Fatigued(float fatigueCost)
    {
        fatigue += fatigueCost;
    }

    public void Healed(float healthRestored)
    {
        currentHealth += healthRestored;
    }
}
