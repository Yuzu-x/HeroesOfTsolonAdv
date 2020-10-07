using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public bool currentTile = false;
    public bool targetTile = false;
    public bool selectableTile = false;
    public bool walkableTile = true;
    public bool playerSpawnTile = false;

    public List<TileController> adjacentTileList = new List<TileController>();

    public bool visitedTile = false;
    public TileController parentTile = null;
    public int distance = 0;

    public float f = 0;
    public float g = 0;
    public float h = 0;

    void Update()
    {
        if(currentTile)
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
        else if(targetTile)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if(selectableTile)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {
        adjacentTileList.Clear();
        currentTile = false;
        targetTile = false;
        selectableTile = false;
        visitedTile = false;
        parentTile = null;
        distance = 0;

        f = g = h = 0;
    }

    public void FindNeighbours(float jumpHeight, TileController target)
    {
        Reset();
        CheckTile(Vector3.forward, jumpHeight, target);
        CheckTile(-Vector3.forward, jumpHeight, target);
        CheckTile(Vector3.right, jumpHeight, target);
        CheckTile(-Vector3.right, jumpHeight, target);
    }

    public void CheckTile(Vector3 direction, float jumpHeight, TileController target)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach(Collider item in colliders)
        {
            TileController tile = item.GetComponent<TileController>();

            if(tile != null && tile.walkableTile)
            {
                RaycastHit hit;

                if(!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target))
                {
                    adjacentTileList.Add(tile);
                }
            }
        }
    }
}
