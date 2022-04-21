using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuildingPlacement
{
    VALID,
    INVALID,
    FIXED
};
public class Building
{
    private BuildingData _data;
    private Transform _transform;
    private int _currentHealth;
    private BuildingPlacement _placement;
    private List<Material> _materials;
    private BuildingManager _buildingManager;
    public Building(BuildingData data)
    {
        _data = data;
        _currentHealth = data.healthpoints;

        

        GameObject g = GameObject.Instantiate(data.prefab) as GameObject;
        _transform = g.transform;
        _buildingManager = _transform.GetComponent<BuildingManager>();
        _materials = new List<Material>();
        foreach (Material material in _transform.Find("Mesh").GetComponent<Renderer>().materials)
        {
            _materials.Add(new Material(material));
        }

        _placement = BuildingPlacement.VALID;
        SetMaterials();
    }
    public void SetMaterials() { SetMaterials(_placement); }
    public void SetMaterials(BuildingPlacement placement)
    {
        List<Material> materials;
        if (placement == BuildingPlacement.VALID)
        {
            Material refMaterial = Resources.Load("Materials/Valid") as Material;
            materials = new List<Material>();
            for (int i = 0; i < _materials.Count; i++)
            {
                materials.Add(refMaterial);

            }
        }
        else if (placement == BuildingPlacement.INVALID)
        {
            Material refMaterial = Resources.Load("Materials/Invalid") as Material;
            materials = new List<Material>();
            for (int i = 0; i < _materials.Count; i++)
            {
                materials.Add(refMaterial);
            }
        }
        else if (placement == BuildingPlacement.FIXED)
        {
            materials = _materials;
        }
        else
        {
            return;
        }
        _transform.Find("Mesh").GetComponent<Renderer>().materials = materials.ToArray();
    }
    public void Place()
    {
        // set placement state
        _placement = BuildingPlacement.FIXED;
        
        //change building materials
        SetMaterials();
        //remove "is trigger" flag from the box collidor to allow for collision with units
        _transform.GetComponent<BoxCollider>().isTrigger = false;
        //update game resources: remove the cost of the building
        //from each game resource
        foreach (ResourceValue resource in _data.cost)
        {
            Globals.GAME_RESOURCES[resource.code].AddAmount(-resource.amount);
        }

    }
    public bool CanBuy()
    {
        return _data.CanBuy();
    }

    public void CheckValidPlacement()
    {
        if (_placement == BuildingPlacement.FIXED) return;
        _placement = _buildingManager.CheckPlacement()
            ? BuildingPlacement.VALID
            : BuildingPlacement.INVALID;

    }

    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }
    public bool HasValidPlacement { get => _placement == BuildingPlacement.VALID; }
    public bool IsFixed { get => _placement == BuildingPlacement.FIXED; }
    public string Code { get => _data.code; }
    public Transform Transform { get => _transform; }
    public int HP { get => _currentHealth; set => _currentHealth = value; }
    public int MaxHP { get => _data.healthpoints;  }
    public int DataIndex
    {
        get
        {
            for (int i = 0; i < Globals.BUILDING_DATA.Length; i++)
            {
                if (Globals.BUILDING_DATA[i].code == _data.code)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

