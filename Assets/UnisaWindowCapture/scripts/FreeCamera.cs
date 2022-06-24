//This part of the code references and partially edits the work of the following links
//to realize the movement of the perspective in the scene in this project
// https://www.cnblogs.com/machine/p/unity.html

//This script was not used in the final project, but can still be activated in the "MainCamera" component.
//Unlike the current cube-centric camera control scheme, this script can control the free movement of the camera.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    // Model
    public Transform car_model;
    // Spinning speed
    public static float rotateSpeed = 10f;  
    public static float rotateLerp = 8;
    // Moving speed
    public static float moveSpeed = 0.5f;
    public static float moveLerp = 10f;
    // Lens stretch speed 
    public static float zoomSpeed = 10f;   
    public static float zoomLerp = 5f;

    // Calculate movement, rotation and distance. 
    private Vector3 position, targetPosition;
    private Quaternion rotation, targetRotation;
    private float distance, targetDistance;
  
    private const float default_distance = 2f;


    void Start()
    {
        // initialization. 
        targetRotation = Quaternion.identity;     
        targetPosition = new Vector3(0.59f, 1.62f, -11.21f);     
        targetDistance = default_distance;
    }


    void Update()
    {
        //Debug.Log("camera button ");
        float dx = Input.GetAxis("Mouse X");
        float dy = Input.GetAxis("Mouse Y");

        // left mouse button movement. 
        if (Input.GetMouseButton(0))
        {
            dx *= moveSpeed;
            dy *= moveSpeed;
            targetPosition -= transform.up * dy + transform.right * dx;
        }

        // Right mouse button to rotate.
        if (Input.GetMouseButton(1))
        {
            dx *= rotateSpeed;
            dy *= rotateSpeed;
            if (Mathf.Abs(dx) > 0 || Mathf.Abs(dy) > 0)
            {
                // Get camera Euler angles. 
                Vector3 angles = transform.rotation.eulerAngles;
            
                angles.x = Mathf.Repeat(angles.x + 180f, 360f) - 180f;
                angles.y += dx;
                angles.x -= dy;
                // Calculate camera rotation. 
                targetRotation.eulerAngles = new Vector3(angles.x, angles.y, 0);
            }
        }

        // Mouse wheel stretch. 
        targetDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
    }

    private void FixedUpdate()
    {
        rotation = Quaternion.Slerp(rotation, targetRotation, Time.deltaTime * rotateLerp);
        position = Vector3.Lerp(position, targetPosition, Time.deltaTime * moveLerp);
        distance = Mathf.Lerp(distance, targetDistance, Time.deltaTime * zoomLerp);
        // Set camera rotation. 
        transform.rotation = rotation;
        // Set the camera position. 
        transform.position = position - rotation * new Vector3(0, 0, distance);
    }
}