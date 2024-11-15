using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractactionType
    {
        door,
        button,
        pickup
    }

    public InteractactionType type;
    public GameObject targetGameObject;
    public PickupStats pickupStats;

    public void Activate()
    {
        switch (type)
        {
            case InteractactionType.door:
                gameObject.SetActive(false);
                break;

            case InteractactionType.button:
                targetGameObject.SetActive(!targetGameObject.activeSelf);
                break;

            case InteractactionType.pickup:
                gameObject.SetActive(false);
                Debug.Log("Picked up a " + pickupStats.GetName() + " Pickup and got " + pickupStats.GetValue() + " points!");
                break;
        }
    
    }
}
