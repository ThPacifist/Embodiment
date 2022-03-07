using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapColorChanger : GameAction
{
    public Color offColor, onColor;
    public TilemapRenderer tileMapRenderer;

    public void OnEnable() 
    { 
        Reset(); 
    }
    public override void Action(bool b)
    {
        tileMapRenderer.material.color = onColor;
    }
    public void Reset() 
    {
        tileMapRenderer.material.color = offColor; 
    }

}
