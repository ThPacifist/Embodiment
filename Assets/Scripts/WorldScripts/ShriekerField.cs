using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShriekerField : MonoBehaviour
{
    [SerializeField]
    GameAction behavior;
    PlyController temp2;
    AudioManager audioManager;

    //If the player is this field, destory the skeleton
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(GameAction.PlayerTags(other.tag))//If it's the player
        {
            if (other.gameObject.layer == 6)
            {
                ControlMovement temp = other.GetComponent<ControlMovement>();
                temp2 = other.GetComponent<PlyController>();
                if (temp.heldSkeleton != null || temp.skeleton != null)
                {
                    temp2.canMove = false;
                    temp2.canJump = false;
                    DestroySkeleton(temp);
                    StartCoroutine("haltMovement");
                    //Commented out until the sound exists
                    /*
                    if(audioManager != null)
                    {
                        audioManager.Play("shriekerShriek");
                    }
                    */
                }
            }

            //Any other behavior
            //behavior.Action();
        }
    }

    IEnumerator haltMovement()
    {
        yield return new WaitForSeconds(2);
        temp2.canMove = true;
        temp2.canJump = true;
    }

    void DestroySkeleton(ControlMovement ply)
    {
        ply.DestorySkeleton();
    }
}
