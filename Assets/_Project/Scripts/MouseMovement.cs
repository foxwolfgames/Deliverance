using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    float topClampAngle = -90f;
    float bottomClampAngle = 90f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Getting mouse input 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotating the player around the x axis (look up and down)
        xRotation -= mouseY;

        //clamp the rotation
        xRotation = Mathf.Clamp(xRotation, topClampAngle, bottomClampAngle);

        //Rotating the player around the y axis (look left and right)
        yRotation += mouseX;

        //Applying the rotation
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
