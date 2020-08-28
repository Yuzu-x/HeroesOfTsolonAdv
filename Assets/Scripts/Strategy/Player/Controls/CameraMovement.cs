using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraMovementSpeed = 2f;


    void Update()
    {
        Vector3 cameraTransformF = transform.forward;
        Vector3 cameraTransformR = transform.right;

        Vector2 camInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        camInput.Normalize();
        transform.position += (cameraTransformF * camInput.y + cameraTransformR * camInput.x) * cameraMovementSpeed * Time.deltaTime;
    }
}
