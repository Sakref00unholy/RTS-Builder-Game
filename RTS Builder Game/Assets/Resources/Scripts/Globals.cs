using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public static BuildingData[] BUILDING_DATA = new BuildingData[]
    {
        new BuildingData("House", 100, new Dictionary<string, int>()
        {
            {"gold", 100 },
            {"wood", 120 },
        }),
        new BuildingData("Tower", 150, new Dictionary<string, int>()
        {
            {"gold", 80},
            {"wood", 80 },
            {"stone", 100 }
        })
    };
    public static Dictionary<string, GameResource> GAME_RESOURCES = new Dictionary<string, GameResource>()
    {
        {"gold", new GameResource("Gold", 500) },
        {"wood", new GameResource("Wood", 500) },
        {"stone", new GameResource("Stone", 250) }
    };

    public static int TERRAIN_LAYER_MASK = 1 << 8;

}
