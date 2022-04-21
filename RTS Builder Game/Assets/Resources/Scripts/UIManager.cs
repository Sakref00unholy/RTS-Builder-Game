using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private BuildingPlacer _buildingPlacer;

    public Transform buildingMenu;
    public GameObject buildingButtonPrefab;
    public Transform resourcesUIParent;
    public GameObject gameResourceDisplayPrefab;

    public GameObject infoPanel;
    private Text _infoPanelTitleText;
    private Text _infoPanelDescriptionText;
    private Transform _infoPanelResourcesCostParent;

    private Dictionary<string, Text> _resourceTexts;
    private Dictionary<string, Button> _buildingButtons;


    private void Awake()
    {
        Transform infoPanelTransform = infoPanel.transform;
        _infoPanelTitleText = infoPanelTransform.Find("Content/Title").GetComponent<Text>();
        _infoPanelDescriptionText = infoPanelTransform.Find("Content/Description").GetComponent<Text>();
        _infoPanelResourcesCostParent = infoPanelTransform.Find("Content/ResourcesCost");
        //create texts for each in-game resource (gold, wood, stone...)
        _resourceTexts = new Dictionary<string, Text>();
        foreach (KeyValuePair<string, GameResource> pair in Globals.GAME_RESOURCES)
        {
            GameObject display = Instantiate(gameResourceDisplayPrefab);
            display.name = pair.Key;
            _resourceTexts[pair.Key] = display.transform.Find("Text").GetComponent<Text>();
            _SetResourceText(pair.Key, pair.Value.Amount);
            display.transform.SetParent(resourcesUIParent);

        }


        _buildingPlacer = GetComponent<BuildingPlacer>();
        //create buttons for each building type


        _buildingButtons = new Dictionary<string, Button>();
        for (int i = 0; i < Globals.BUILDING_DATA.Length; i++)
        {
            BuildingData data = Globals.BUILDING_DATA[i];
            GameObject button = Instantiate(buildingButtonPrefab);
            button.name = data.unitName;
            button.transform.Find("Text").GetComponent<Text>().text = data.unitName;
            Button b = button.GetComponent<Button>();
            _buildingButtons[data.code] = b;
            _AddBuildingButtonListener(b, i);
            button.transform.SetParent(buildingMenu);
            button.GetComponent<BuildingButton>().Initialize(Globals.BUILDING_DATA[i]);

            if (!Globals.BUILDING_DATA[i].CanBuy())
            {
                b.interactable = false;
            }
        }
        _ShowInfoPanel(false);
    }
   
    public void CheckBuildingButtons()
    {
        foreach (BuildingData data in Globals.BUILDING_DATA)
        {
            _buildingButtons[data.code].interactable = data.CanBuy();
        }
    }

    private void _SetResourceText(string resource, int value)
    {
        _resourceTexts[resource].text = value.ToString();
    }

    public void UpdateResourceTexts()
    {
        foreach (KeyValuePair<string, GameResource> pair in Globals.GAME_RESOURCES)
        {
            _SetResourceText(pair.Key, pair.Value.Amount);
        }
    }

    private void _AddBuildingButtonListener(Button b, int i)
    {
        b.onClick.AddListener(() => _buildingPlacer.SelectPlacedBuilding(i));
    }
    private void OnEnable()
    {
        EventManager.AddListener("UpdateResourceTexts", _OnUpdateResourceTexts);
        EventManager.AddListener("CheckBuildingButtons", _OnCheckBuildingButtons);
        EventManager.AddTypedListener("HoverBuildingButton", _OnHoverBuildingButton);
        EventManager.AddListener("UnhoverBuildingButton", _OnUnhoverBuildingButton);

    }
    private void OnDisable()
    {
        EventManager.RemoveListener("UpdateResourceTexts", _OnUpdateResourceTexts);
        EventManager.RemoveListener("CheckBuildingButtons", _OnCheckBuildingButtons);
        EventManager.RemoveTypedListener("HoverBuildingButton", _OnHoverBuildingButton);
        EventManager.RemoveListener("UnhoverBuildingButton", _OnUnhoverBuildingButton);

    }
    private void _OnUpdateResourceTexts()
        {
           foreach (KeyValuePair<string, GameResource> pair in Globals.GAME_RESOURCES)
            _SetResourceText(pair.Key, pair.Value.Amount); 
        }
    private void _OnCheckBuildingButtons()
    {
       foreach (BuildingData data in Globals.BUILDING_DATA)
            {
                _buildingButtons[data.code].interactable = data.CanBuy();
            } 

    }
    public void _SetInfoPanel(BuildingData data)
    {
        // update texts
        if (data.Code != "") _infoPanelTitleText.text = data.Code;
        if (data.Description != "") _infoPanelDescriptionText.text = data.Description;
        // clear resource costs and reinstantiate new ones
        foreach (Transform child in _infoPanelResourcesCostParent) Destroy(child.gameObject);
        if (data.Cost.Count > 0)
        {
            GameObject g; Transform t;
            foreach (ResourceValue resource in data.Cost)
            {
                g = Instantiate(GameResourceCost) as GameObject;
                t = g.transform;
                t.Find("Text").GetComponent<Text>().text = resource.amount.ToString();
                t.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Textures/GameResources/{resource.code}");
                t.SetParent(_infoPanelResourcesCostParent);
            }
        }
    }

    public void _ShowInfoPanel(bool show)
    {
        infoPanel.SetActive(show);
    }

}
