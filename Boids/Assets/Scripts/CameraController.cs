using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    public float speed = 10.0f;

    private float offsetX = 0f;
    private float offsetY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    { 
        // Camera translation
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        movement = transform.worldToLocalMatrix.inverse * movement;
        transform.position = transform.position + movement;
       
       
        // Camera rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        offsetX += mouseX * mouseSensitivity;
        offsetY += mouseY * mouseSensitivity;
            
        Quaternion rotation = Quaternion.Euler(-offsetY, offsetX, 0f);
        transform.rotation = rotation;
    }
}


