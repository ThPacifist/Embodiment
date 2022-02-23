using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpozyteMoveWithPlayer : MonoBehaviour
{
    /*
     * Description:
     * This trigger begins Expozyte's move with player behavior
     */

    //Public Variables
    public ExpozyteMove ExpMove;
    public int goToCheckpoint;
    public bool left;

    //When the player enters move
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Begin move
        ExpMove.BeginMove(goToCheckpoint);
        this.gameObject.SetActive(false);
    }
}
