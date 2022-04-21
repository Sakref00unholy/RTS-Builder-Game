using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BuildingData _buildingData;

    public void Initialize(BuildingData buildingData)
    {
        _buildingData = buildingData;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.TriggerTypedEvent("HoverBuildingButton", new CustomEventData(_buildingData));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventManager.TriggerEvent("UnhoverBuildingButton");
    }
}
