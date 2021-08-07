﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHandler { 
    public static void LoadGameData()
    {
        Globals.BUILDING_DATA = Resources.LoadAll<BuildingData>("ScriptableObject/Units/Buildings") as BuildingData[];
    }
    

}
