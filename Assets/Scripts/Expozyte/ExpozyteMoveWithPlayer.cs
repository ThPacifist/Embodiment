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
    public int speed;
    public bool left;

    //When the player enters move
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Begin move
        ExpMove.MoveWithPlayer(left, goToCheckpoint, speed);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
