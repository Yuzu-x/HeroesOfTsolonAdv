using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPieceAction : MonoBehaviour
{
    public GameObject heldPiece;

    void Update()
    {
        if(heldPiece != null)
        {
            Vector2 piecePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            heldPiece.transform.position = piecePos;
        }
    }
}
