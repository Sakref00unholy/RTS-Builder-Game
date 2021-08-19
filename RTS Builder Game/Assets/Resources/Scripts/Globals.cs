using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public static BuildingData[] BUILDING_DATA;
    public static Dictionary<string, GameResource> GAME_RESOURCES = new Dictionary<string, GameResource>()
    {
        {"gold", new GameResource("Gold", 500) },
        {"wood", new GameResource("Wood", 500) },
        {"stone", new GameResource("Stone", 250) }
    };

    public static int TERRAIN_LAYER_MASK = 1 << 8;
    public static List<UnitManager> SELECTED_UNITS = new List<UnitManager>();
    

}
