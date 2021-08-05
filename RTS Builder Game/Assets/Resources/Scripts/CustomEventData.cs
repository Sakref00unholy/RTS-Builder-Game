using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEventData
{
    public BuildingData buildingData;

    public CustomEventData(BuildingData buildingData)
    {
        this.buildingData = buildingData;
    }
}

[System.Serializable]
public class CustomEvent : UnityEvent<CustomEventData> { }
