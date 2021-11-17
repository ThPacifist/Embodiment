using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    [SerializeField]
    Transform gObject;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    Transform endPos;

    [SerializeField]
    //Set this to true to have the gObjects position slowly move back to it restPos
    bool Decay;
    [SerializeField]
    float decaySpeed = 1;

    PlyController plyCntrl;
    Vector3 restPos;

    private void Awake()
    {
        restPos = gObject.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Decay)
        {
            if (plyCntrl != null)
            {
                //If the player is move left on the treadmill, move the gObject towards the endPos
                if (plyCntrl.Left)
                {
                    UpdateGameObject('+');
                }
                //If the player moves right, move the gObject towards the restPos
                else if (plyCntrl.Right)
                {
                    UpdateGameObject('-');
                }
            }
        }
        else
        {
            if (plyCntrl != null)
            {
                if (plyCntrl.Left)
                {
                    UpdateGameObject('+');
                }
                else if (plyCntrl.Right)
                {
                    UpdateGameObject('-');
                }
                else
                {
                    DecayPos();
                }
            }
            else
            {
                DecayPos();
            }
        }
    }

    //When "+" is passed it moves the gObject towards the endPos, when "-" is passed it moves it away 
    //from the endPos back towards its restPos
    void UpdateGameObject(char value)
    {
        if(value == '+')
        {
            if (Vector2.Distance(gObject.position, endPos.position) > 0.001)
            {
                gObject.position = Vector2.MoveTowards(gObject.position, endPos.position, Time.deltaTime * speed);
            }
            //When gObject is at the endPos, unlock the player
            else
            {
                plyCntrl.move = true;
            }
        }
        else if(value == '-')
        {
            if (Vector2.Distance(gObject.position, restPos) > 0.001)
            {
                gObject.position = Vector2.MoveTowards(gObject.position, restPos, Time.deltaTime * speed);
            }
            //When gObject is at the restPos, unlock the player
            else
            {
                plyCntrl.move = true;
            }
        }
    }

    //Moves the gObject back towards its restPos overTime
    void DecayPos()
    {
        if (Vector2.Distance(gObject.position, restPos) > 0.001)
        {
            gObject.position = Vector2.MoveTowards(gObject.position, restPos, Time.deltaTime * decaySpeed);
        }
    }

    public void SetPlyCntrl(PlyController ply)
    {
        plyCntrl = ply;
    }
}
