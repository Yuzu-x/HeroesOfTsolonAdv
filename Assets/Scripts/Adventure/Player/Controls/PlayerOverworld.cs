using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworld : MonoBehaviour
{
    public Rigidbody playerRigid;

    public float moveSpeed = 6f;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 overWorldHeading = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));

        //playerRigid.AddForce(overWorldHeading * (moveSpeed * Time.deltaTime));
        this.transform.Translate(overWorldHeading * moveSpeed * Time.deltaTime);

        if(overWorldHeading == Vector3.zero)
        {
            playerRigid.velocity = Vector3.zero;
            playerRigid.angularVelocity = Vector3.zero;
        }
    }
}
