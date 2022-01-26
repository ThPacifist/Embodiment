using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapColorChanger : GameAction
{
    public Color offColor, onCOlor;
    public Tilemap tileMap;


    public void OnEnable() { Reset(); }
    public override void Action(bool b)
    {
        tileMap.color = onCOlor;
    }
    public void Reset() { tileMap.color = offColor; }

}
