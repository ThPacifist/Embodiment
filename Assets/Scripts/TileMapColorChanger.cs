using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapColorChanger : GameAction
{
    public Color offColor = new Color(0.047f, 0.27f, 0.93f, 1);
    public Color onColor = new Color(0.047f, 0.82f, 0.93f, 1);
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
