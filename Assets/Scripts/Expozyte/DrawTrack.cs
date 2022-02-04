using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrack : MonoBehaviour
{
    /*
     * Description:
     * This draws the track for expozyte to move on
     * It can be modified to draw other lines as well
     */

    //Public variables
    public LineRenderer line;
    public ExpozyteMove expoMove;

    //Private variables
    private Transform[] points;

    //Runs on start
    private void Start()
    {
        //Initialize th array for convienance
        points = expoMove.Checkpoints;

        //Set the line's point size
        line.positionCount = points.Length;

        //Set the points to be the points in the renderer
        for(int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i, points[i].position);
        }
    }
}
