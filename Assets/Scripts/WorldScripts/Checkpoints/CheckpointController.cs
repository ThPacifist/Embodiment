using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckpointController : MonoBehaviour
{
    /*
     * Description:
     * This script sends the player to the previous checkpoint when needed by other scripts
     */

    //Pubilc variables

    public Transform[] checkpoints;
    public Transform player;
    public int toCheckpoint = 0;
    public static Action RebuildData = delegate{ };

    [SerializeField]
    PlyController plyCntrl;

    [SerializeField]
    TransitionController transCntrl;

    //Private variables
    public int previousCheckpoint = 0;
    Animator plyAnim;

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        Checkpoint.newCheckpoint += UpdateCheckpoint;
        TransitionController.slideOutAction += RespawnPlayer;
        TransitionController.slideInAction += EnableMovement;
    }

    private void OnDisable()
    {
        Checkpoint.newCheckpoint -= UpdateCheckpoint;
        TransitionController.slideOutAction -= RespawnPlayer;
        TransitionController.slideInAction -= EnableMovement;
    }

    //Do when awake
    private void Awake()
    {
        plyAnim = player.GetComponent<Animator>();
        //Debug.Log("Animator is " + plyAnim);
    }

    //Update
    //Debug mode change checkpoints, add slashes to * for testing
    /*
     *
    private void FixedUpdate()
    {
        if(toCheckpoint != previousCheckpoint)
        {
            player.position = checkpoints[toCheckpoint].position;
            previousCheckpoint = toCheckpoint;
        }
    }
    *
    */

    //Gets called whenever the player gets moved to a new checkpoint
    public void MoveToCheckpoint(int newPosition)
    {
        player.position = checkpoints[newPosition].transform.position;
    }

    //Update checkpoint number when a new checkpoint is touched
    public void UpdateCheckpoint(int newPosition)
    {
        if (newPosition > previousCheckpoint)
        {
            previousCheckpoint = newPosition;
            RebuildData();
        }
    }

    //When the player dies move them to their last checkpoint,
    //disable movement, and play a screen transition
    void RespawnPlayer()
    {
        MoveToCheckpoint(previousCheckpoint);
        player.GetComponent<SpriteRenderer>().enabled = true;
        //Try to place the player on the ground
        player.position = GameAction.PlaceColOnGround(player.GetComponent<Collider2D>());
        plyAnim.SetTrigger(player.tag);
    }

    //When ready re-enable movement for the player after a delay
    void EnableMovement()
    {
        StartCoroutine(Delay());
    }

    //Wait a second, and turn on movement
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        PlayerBrain.PB.canMove = true;
        Debug.DrawLine(player.position, player.position + Vector3.up, Color.white, 2f);
    }
}
