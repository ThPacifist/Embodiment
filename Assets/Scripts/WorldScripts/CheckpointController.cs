using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    TransitionController transCntrl;

    //Private variables
    public int previousCheckpoint = 0;
    Animator plyAnim;

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        Checkpoint.newCheckpoint += UpdateCheckpoint;
    }

    private void OnDisable()
    {
        Checkpoint.newCheckpoint -= UpdateCheckpoint;
    }

    private void Awake()
    {
        plyAnim = player.GetComponent<Animator>();
    }

    //Update
    private void Update()
    {
        //Debug mode change checkpoints, add slashes to * for testing
        /*
         *
        if(toCheckpoint != previousCheckpoint)
        {
            player.position = checkpoints[toCheckpoint].position;
            previousCheckpoint = toCheckpoint;
        }
        *
        */
        if(!player.gameObject.activeSelf)
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    //Gets called whenever the player gets moved
    public void MoveToCheckpoint(int newPosition)
    {
        player.position = checkpoints[newPosition].transform.position;
    }

    //Update checkpoint number
    public void UpdateCheckpoint(int newPosition)
    {
        previousCheckpoint = newPosition;
    }

    IEnumerator RespawnPlayer()
    {
        transCntrl.SlideOut();
        MoveToCheckpoint(previousCheckpoint);
        player.gameObject.SetActive(true);
        plyAnim.SetTrigger(player.tag);
        while (!transCntrl.TriggerSlideIn)
        {
            yield return null;
        }
        transCntrl.SlideIn();
    }
}
