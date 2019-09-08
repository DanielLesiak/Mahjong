using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tile")]
public class Tile_SO : ScriptableObject
{

    public TileType tileType;

    [Range(1,9)]
    public int numberValue;
    public Value_Winds windValue;
    public Value_Dragons dragonValue;

    public Sprite sprite;
    public int uniqueTileID;

}

public enum TileType { Pins, Bamboos, Characters, Winds, Dragons };
public enum Value_Winds { North, Sounth, West, East };
public enum Value_Dragons { White, Green, Red };
