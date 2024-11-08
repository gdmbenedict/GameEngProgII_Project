using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class InputManager : MonoBehaviour
{
    // Script References
    [SerializeField] private PlayerLocomotionHandler playerLocomotionHandler;
    [SerializeField] private CameraManager cameraManager; // Reference to CameraManager

    [Header("Movement Inputs")]
    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput;
    public Vector2 movementInput;
    public float moveAmount;

    [Header("Camera Inputs")]
    public float scrollInput; // Scroll input for camera zoom
    public Vector2 cameraInput; // Mouse input for the camera

    public bool isPauseKeyPressed = false;
    public bool isSprinting = false;

    [Header("Object References")]
    [SerializeField] private InteractionManager interactionManager;


    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleCameraInput();
        HandlePauseKeyInput();
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void GetJumpInput(InputAction.CallbackContext context)
    {
        jumpInput = context.performed;
    }

    public void GetPauseInput(InputAction.CallbackContext context)
    {
        //way pause is handled is not working well

        /*
        if (context.performed)
        {
            isPauseKeyPressed = true;
        }
        else
        {
            isPauseKeyPressed = false;
        }
        */
    }

    public void GetSprintInput(InputAction.CallbackContext context)
    {
        isSprinting = context.action.IsPressed();
    }

    public void GetZoom(InputAction.CallbackContext context)
    {
        scrollInput = context.ReadValue<float>();
    }

    public void GetLook(InputAction.CallbackContext context)
    {
        cameraInput = context.ReadValue<Vector2>();
    }

    public void GetInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && interactionManager.GetCanInteract())
        {
            Debug.Log("Input accepted");
            interactionManager.Interact();
        }
    }

    private void HandleCameraInput()
    {        
            // Get mouse input for the camera
            //cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            // Get scroll input for camera zoom
            //scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // Send inputs to CameraManager
            cameraManager.zoomInput = scrollInput;
            cameraManager.cameraInput = cameraInput;        
    }

    
    private void HandleMovementInput()
    {
        //movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }

    private void HandlePauseKeyInput()
    {
        isPauseKeyPressed = Input.GetKeyDown(KeyCode.Escape); // Detect the escape key press
    }

    private void HandleSprintingInput()
    {
        if (isSprinting && moveAmount > 0.5f)
        {
            playerLocomotionHandler.isSprinting = true;
        }
        else
        {
            playerLocomotionHandler.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        //jumpInput = Input.GetKeyDown(KeyCode.Space); // Detect jump input (spacebar)
        if (jumpInput)
        {
            playerLocomotionHandler.HandleJump(); // Trigger jump in locomotion handler
        }
    }





}
