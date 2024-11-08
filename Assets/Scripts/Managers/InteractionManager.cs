using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private CameraManager camManager;
    private Camera playerCam;
    private UIManager uiManager;
    private GameObject target;
    private Interactable targetInteractable;
    [SerializeField] private float maxRayDistance;
    [SerializeField] private LayerMask interactableLayer;

    private bool canInteract;

    private void Awake()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        camManager = FindAnyObjectByType<CameraManager>();
        playerCam = camManager.playerCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, maxRayDistance))
        {
            //bit shifting to figure out if layer is in layermask?
            if ((interactableLayer & (1 << hit.transform.gameObject.layer)) != 0)
            {
                //Debug.Log("Looking at " + hit.transform.gameObject.name);
                target = hit.transform.gameObject;
                targetInteractable = target.GetComponent<Interactable>();
            }
            else
            {
                target = null;
                targetInteractable = null;
            }
            SetGameplayMessage();
        }
    }

    public void Interact()
    {
        targetInteractable.Activate(); 
    }

    private void SetGameplayMessage()
    {
        string message = "";

        if (target == null)
        {
            uiManager.UpdateGameplayMessage(message);
            return;
        }

        switch (targetInteractable.type)
        {
            case Interactable.InteractactionType.door:
                message = "Press LMB to open door";
                break;

            case Interactable.InteractactionType.button:
                message = "Press LMB to press button";
                break;

            case Interactable.InteractactionType.pickup:
                message = "Press LMB to pick up " + targetInteractable.name +".";
                break;
        }

        uiManager.UpdateGameplayMessage(message);
    }

    public bool GetCanInteract()
    {
        return canInteract;
    }
}
