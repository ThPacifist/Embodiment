using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{

    public enum Direction
    {
        Right,
        Left
    }

    [SerializeField]
    Animator anim;
    [SerializeField]
    Transform affectedObject;
    [SerializeField]
    Transform endPos;
    [SerializeField]
    float speed = 1;
    [Tooltip("Represents the direction the player needs to move.\n" +
        "Note: Make sure to flip the Sprite Renderer along the x axis")]
    public Direction direction;
    public bool isAttached;
    public float range = 0;

    /// <summary>
    /// Makes the affectedObject return it's original position over time
    /// </summary>
    [Tooltip("Makes the affectedObject return it's original position over time.")]
    [Space]
    [SerializeField]
    bool Decay;
    public float decaySpeed = 1;

    CatController cat;
    Vector3 restPos;

    private void Awake()
    {
        anim.SetFloat("Speed", 0);
        if (affectedObject != null)
        restPos = affectedObject.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isAttached && cat != null)
        {
            if(cat.transform.position.x < (transform.position.x + range) && cat.transform.position.x > (transform.position.x - range))
            {
                isAttached = true;
                cat.treadmill = true;
                cat.transform.position = new Vector3(transform.position.x, cat.transform.position.y, cat.transform.position.z);
                PlayerBrain.PB.rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }


        if (!Decay)
        {
            if (isAttached && cat != null)
            {
                //If direction is set to Right
                if (direction == Direction.Right)
                {
                    //If the player is move left on the treadmill, move the affected object towards the endPos
                    if (cat.Right)
                    {
                        UpdateGameObject('+');
                        anim.SetFloat("Speed", 1);
                    }
                    //If the player moves right, move the affected object towards the restPos
                    else if (cat.Left)
                    {
                        UpdateGameObject('-');
                        anim.SetFloat("Speed", -1);
                    }
                    else
                    {
                        anim.SetFloat("Speed", 0);
                    }
                }
                //If direction is set to Left
                else if(direction == Direction.Left)
                {
                    //If the player is move right on the treadmill, move the affected object towards the restPos
                    if (cat.Right)
                    {
                        UpdateGameObject('-');
                        anim.SetFloat("Speed", -1);
                    }
                    //If the player moves left, move the affected object towards the endPos
                    else if (cat.Left)
                    {
                        UpdateGameObject('+');
                        anim.SetFloat("Speed", -1);
                    }
                    else
                    {
                        anim.SetFloat("Speed", 0);
                    }
                }
            }
            else
            {
                anim.SetFloat("Speed", 0);
            }
        }
        else
        {
            if (isAttached && cat != null)
            {
                if (cat.Left)
                {
                    UpdateGameObject('+');
                    anim.SetFloat("Speed", 1);
                }
                else if (cat.Right)
                {
                    UpdateGameObject('-');
                    anim.SetFloat("Speed", -1);
                }
                else
                {
                    DecayPos();
                    anim.SetFloat("Speed", -1);
                }
            }
            else
            {
                DecayPos();
                anim.SetFloat("Speed", -1);
            }
        }
    }

    //When "+" is passed it moves the gObject towards the endPos, when "-" is passed it moves it away 
    //from the endPos back towards its restPos
    void UpdateGameObject(char value)
    {
        if(value == '+')
        {
            if (Vector2.Distance(affectedObject.position, endPos.position) > 0.001)
            {
                affectedObject.position = Vector2.MoveTowards(affectedObject.position, endPos.position, Time.deltaTime * speed);
            }
            //When gObject is at the endPos, unlock the player
            else
            {
                DisconnectCat();
            }
        }
        else if(value == '-')
        {
            if (Vector2.Distance(affectedObject.position, restPos) > 0.001)
            {
                affectedObject.position = Vector2.MoveTowards(affectedObject.position, restPos, Time.deltaTime * speed);
            }
            //When gObject is at the restPos, unlock the player
            else
            {
                DisconnectCat();
            }
        }
    }

    //Moves the gObject back towards its restPos overTime
    void DecayPos()
    {
        if (Vector2.Distance(affectedObject.position, restPos) > 0.001)
        {
            affectedObject.position = Vector2.MoveTowards(affectedObject.position, restPos, Time.deltaTime * decaySpeed);
        }
    }

    public void SetCatCntrl(CatController kit)
    {
        cat = kit;
    }

    void DisconnectCat()
    {
        isAttached = false;
        cat.treadmill = false;
        cat = null;
        PlayerBrain.PB.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        CatController.JumpAction -= DisconnectCat;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Cat"))
        {
            cat = collision.collider.GetComponent<CatController>();
            CatController.JumpAction += DisconnectCat;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cat"))
        {
            CatController.JumpAction -= DisconnectCat;
            PlayerBrain.PB.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            cat = null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 center = transform.position + Vector3.up;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(center + new Vector3(-0.5f, 0, 0), center + new Vector3(0.5f, 0, 0));
        if (direction == Direction.Right)
        {
            Vector3 tip = center + new Vector3(0.5f, 0, 0);
            Gizmos.DrawLine(tip, tip + new Vector3(-0.25f, 0.25f, 0));
            Gizmos.DrawLine(tip, tip + new Vector3(-0.25f, -0.25f, 0));
            Gizmos.DrawLine(tip + new Vector3(-0.25f, 0.25f, 0), tip + new Vector3(-0.25f, -0.25f, 0));
        }
        else if(direction == Direction.Left)
        {
            Vector3 tip = center + new Vector3(-0.5f, 0, 0);
            Gizmos.DrawLine(tip, tip + new Vector3(0.25f, 0.25f, 0));
            Gizmos.DrawLine(tip, tip + new Vector3(0.25f, -0.25f, 0));
            Gizmos.DrawLine(tip + new Vector3(0.25f, 0.25f, 0), tip + new Vector3(0.25f, -0.25f, 0));
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(transform.position.x + range, transform.position.y + 0.5f, 0), 
            new Vector3(transform.position.x + range, transform.position.y - 0.5f, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x - range, transform.position.y + 0.5f, 0),
            new Vector3(transform.position.x - range, transform.position.y - 0.5f, 0));
    }
}
