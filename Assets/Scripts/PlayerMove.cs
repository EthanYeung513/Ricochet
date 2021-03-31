using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float sprintSpeed = 20f;

    private Vector3 moveInput;
    private Vector3 moveVelocity; // Gets direction AND speed   

    [SerializeField] private Camera cam;

    private void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")); // Gets hori and verti input

        if (Input.GetKey(KeyCode.LeftShift))
        {

            moveVelocity = moveInput * sprintSpeed;
        }
        else
        {
            moveVelocity = moveInput * walkSpeed;
        }


        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(pointToLook);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = moveVelocity; // Applies force
    }
}
