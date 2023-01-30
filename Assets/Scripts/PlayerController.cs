using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    // Holds a reference to the bullet
    public GameObject bullet;

    // References
    CharacterController thisCharacterController; // thisChar thisCtrl
    Transform thisCamera;

    // Player stats
    public float speed = 5;
    public float rotationSpeed = 3;
    public float jumpHeight = 10;
    public float gravity = 6;
    // Camera
    public float cameraAngleLimitUp = 85;
    public float cameraAngleLimitDown = -85;
    public float invertMouseY = -1;
    // Internal variables
    float inpHor;
    float inpVer;
    float inpMouseHor;
    float inpMouseVer;
    
    float cameraRotationVertical;
    Vector3 movementDirection;
    Vector3 jumpDirection;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        // Get the references to the components
        thisCharacterController = GetComponent<CharacterController>();
        thisCamera = Camera.main.transform;
        // Position the camera at eye level
        thisCamera.position = transform.position + (Vector3.up * 1.7f);
        // Reset rotation
        thisCamera.rotation = Quaternion.identity;
        // Parent the camera to the player
        thisCamera.parent = transform;

        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = thisCharacterController.isGrounded;
        

        // Get Input
        inpHor = Input.GetAxis("Horizontal");
        inpVer = Input.GetAxis("Vertical");
        inpMouseHor = Input.GetAxis("Mouse X");
        inpMouseVer = Input.GetAxis("Mouse Y");

        // Movement
        // Rotate player
        transform.Rotate(0, inpMouseHor * rotationSpeed, 0);

        // Direction vector
        movementDirection = (transform.forward * inpVer) + (transform.right * inpHor);
        movementDirection *= speed;



        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5;
        }
       // if (Input.GetKeyDown(Key.Code.LeftControl))
       // {
           
      //  }


        if (isGrounded)
        {
            // Press jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Add jump
                jumpDirection.y = jumpHeight;
            }
        }

        // Add gravity (subtract,but anyway)
        jumpDirection.y -= gravity * Time.deltaTime;

        // Apply player movement
        thisCharacterController.Move(movementDirection * Time.deltaTime);
        
        // Apply gravity and/or jump
        thisCharacterController.Move(jumpDirection * Time.deltaTime);        

        // Apply our input to the angle
        cameraRotationVertical += inpMouseVer * rotationSpeed * invertMouseY;
        // Clamp the value so we don't go over ourselves and invert
        cameraRotationVertical = Mathf.Clamp(cameraRotationVertical, cameraAngleLimitDown, cameraAngleLimitUp);
        // Apply the rotation with simple Euler angles
        thisCamera.localEulerAngles = new Vector3(cameraRotationVertical, 0, 0);

        // Get mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Create a bullet prefab at camera position
            GameObject go = Instantiate(bullet , thisCamera.position , thisCamera.rotation);
            // Ignore collisions between player and bullet
            Physics.IgnoreCollision(go.GetComponent<Collider>(), thisCharacterController);

            
        }
    }
}
