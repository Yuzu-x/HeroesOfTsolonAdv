using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineFirstTurn : MonoBehaviour
{
    public TurnManager turnManager;
    public GameObject pregameCanvas;

    void Update()
    {
        if(turnManager == null)
        {
            GameObject strategyCanvas = GameObject.FindGameObjectWithTag("GameController");
            turnManager = strategyCanvas.GetComponent<TurnManager>();
        }
    }

    public void DetermineFirst()
    {
        turnManager.FirstTurnDetermination();
        pregameCanvas.SetActive(false);
    }
}
