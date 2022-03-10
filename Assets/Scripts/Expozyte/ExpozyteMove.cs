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
    public AnimationCurve animCurve;
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
    private float speed;
    private float rate;

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
            speed = 10;
        }
    }

    //This function moves expozyte
    private void Move()
    {
        //If he is not there yet, move him there
        if(Expozyte.position != Checkpoints[toCheckpoint].position)
        {
            Expozyte.position = Vector2.MoveTowards(Expozyte.position, Checkpoints[toCheckpoint].position, speed * Time.deltaTime);
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
    public void BeginMove(int newCheckpoint, float newSpeed)
    {
        //Set moving and the checkpoint
        if (!moving)
        {
            speed = newSpeed;
            moving = true;
            toCheckpoint = newCheckpoint;
        }
        else
        {
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
        else if (!left && player.position.x > Expozyte.position.x)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (moving)
        {
            //Sets rate based on distance from player on x axis
            float currRate = Mathf.Abs(player.position.x - Expozyte.position.x) / 10;
            rate += Mathf.Clamp(currRate, 0f, 1f);
            //Move expozyte
            Expozyte.position = Vector2.Lerp(Checkpoints[atCheckpoint].position, Checkpoints[toCheckpoint].position, animCurve.Evaluate(rate * Time.deltaTime * speed));
        }
        else
        {
            //Sets rate based on distance from player on x axis
            float currRate = Mathf.Abs(player.position.x - Expozyte.position.x) / 10;
            rate -= Mathf.Clamp(currRate, 0f, 1f);
            //Move expozyte
            Expozyte.position = Vector2.Lerp(Checkpoints[atCheckpoint].position, Checkpoints[toCheckpoint].position, animCurve.Evaluate(rate * Time.deltaTime * speed));
        }

        //Check if he has arrived
        if (Expozyte.position == Checkpoints[toCheckpoint].position)
        {
            moving = false;
            withPlayer = false;
            atCheckpoint = toCheckpoint;
            toCheckpoint = -1;
        }

    }

    //This function handles moving with the player
    public void MoveWithPlayer(bool leftRight, int checkpoint, float speed)
    {
        if (!moving)
        {
            //Set rate
            rate = 0;
            //Sets the direction of movement
            left = leftRight;
            withPlayer = true;
            //Sets checkpoint 
            toCheckpoint = checkpoint;
            PlyMove();
            //Sets speed
            this.speed = speed;
        }
        else
        {
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
        if(queuePos == 0)
        {
        }
        else if(queuePos < 0)
        {
            queuePos = 0;
        }
    }

}