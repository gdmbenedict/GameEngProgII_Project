using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickupStats", menuName = "Stats/PickupStats")]

public class PickupStats : ScriptableObject
{
    public string pickupName;
    public int points;

    public string GetName()
    {
        return pickupName;
    }

    public int GetValue()
    {
        return points;
    }
}
