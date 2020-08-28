using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworld : MonoBehaviour
{
    public Rigidbody2D playerRigid;

    public float moveSpeed = 6f;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 overWorldHeading = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        playerRigid.AddForce(overWorldHeading * (moveSpeed * Time.deltaTime));

        if(overWorldHeading == Vector2.zero)
        {
            playerRigid.velocity = Vector3.zero;
            playerRigid.angularVelocity = 0;
        }
    }
}
