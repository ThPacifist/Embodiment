using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpozyteMove : MonoBehaviour
{
    /*
     * Description:
     * This function handles the movement of Expozyte
     * It is currently in a state of flux due to how early it is
     */

    //Public variables
    public Transform Expozyte;
    public Transform player;
    public int atCheckpoint;
    public int toCheckpoint;
    public bool moving;
    public bool left;
    public bool withPlayer;

    [SerializeField]
    public Transform[] Checkpoints;

    //Private variables
    private int queuePos = 0;
    private int[] queueMove;

    private void Start()
    {
        queueMove = new int[30];
        for (int i = 0; i < 30; i++)
        {
            queueMove[i] = -1;
        }
    }

    //Fixed update is called 60 times a second
    private void FixedUpdate()
    {
        //If he should be moving with the player
        if (withPlayer)
        {
            PlyMove();
        }
        //If he should be moving, move it
        else if (moving)
        {
            Move();
        }
        else if (queueMove[0] != -1)
        {

            toCheckpoint = queueMove[0];
            fixQueue();
            moving = true;
        }
    }

    //This function moves expozyte
    private void Move()
    {
        //If he is not there yet, move him there
        if (Expozyte.position != Checkpoints[toCheckpoint].position)
        {
            Expozyte.position = Vector2.MoveTowards(Expozyte.position, Checkpoints[toCheckpoint].position, 10 * Time.deltaTime);
        }
        //If he is, change his values
        else
        {
            atCheckpoint = toCheckpoint;
            toCheckpoint = -1;
            moving = false;
        }
    }

    //This function sets the movement
    public void BeginMove(int newCheckpoint)
    {
        //Set moving and the checkpoint
        if (!moving)
        {
            moving = true;
            toCheckpoint = newCheckpoint;
        }
        else
        {
            Debug.Log("QueuePos: " + queuePos);// + ". QueueMove: " + queueMove[queuePos]);
            queueMove[queuePos] = newCheckpoint;
            queuePos++;
        }
    }

    //This function handles movement with player
    private void PlyMove()
    {
        //If the player is to the left, and Expozyte requires them to be to the left, move
        if (left && player.position.x < Expozyte.position.x)
        {
            moving = true;
        }
        //If the player is to the right, and he needs them to be to the right, move
        else if (player.position.x > Expozyte.position.x)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (moving)
        {
            Expozyte.position = Vector2.MoveTowards(Expozyte.position, Checkpoints[toCheckpoint].position, 4 * Time.deltaTime);
        }

        //Check if he has arrived
        if (Expozyte.position == Checkpoints[toCheckpoint].position)
        {
            moving = false;
            withPlayer = false;
            toCheckpoint = -1;
        }
    }

    //This function handles moving with the player
    public void MoveWithPlayer(bool leftRight, int checkpoint)
    {
        if (!moving)
        {
            //Sets the direction of movement
            left = leftRight;
            withPlayer = true;
            //Sets checkpoint 
            toCheckpoint = checkpoint;
        }
        else
        {
            Debug.Log("QueuePos: " + queuePos);// + ". QueueMove: " + queueMove[queuePos]);
            queueMove[queuePos] = checkpoint;
            queuePos++;
        }
    }
    //This fixes the queue and puts the next number up front
    private void fixQueue()
    {
        for(int i = 0; i < queuePos; i++)
        {
            queueMove[i] = queueMove[i + 1];
        }
        queuePos--;
    }
}