using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonTrigger : MonoBehaviour
{
    //Public Variables
    public int Form; //This is used in the animator to let it know which form it needs to change to
    public bool isGrabbed = false;
    public string Name;
    public float speed;
    public float jumpHeight;
    public float density;
    public Vector2 colliderSize;
    public Vector2 colliderOffset;
    public CapsuleDirection2D direction;
    public Transform skelGObject;
    public Skeleton skeloScript;

    //Private Variables
    ControlMovement cntrlMove;
    SpecialInteractions spcInter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Blob"))
        {
            cntrlMove = collision.GetComponent<ControlMovement>();
            spcInter = collision.GetComponent<SpecialInteractions>();
            if(cntrlMove != null)
            {
                if (cntrlMove.skeleton == null)
                {
                    cntrlMove.SetEmbodyValues(this);
                }
            }

            if(spcInter != null)
            {
                if (spcInter.heldSkel == null)
                {
                    spcInter.SetHeldSkel(this);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Blob"))
        {
            if (cntrlMove != null)
            {
                if (!isGrabbed)
                {
                    if (cntrlMove.skeleton == this)
                    {
                        cntrlMove.SetEmbodyValues(null);
                        cntrlMove = null;
                    }
                }
            }

            if (spcInter != null)
            {
                if (!isGrabbed)
                {
                    if (spcInter.skeleton == this)
                    {
                        spcInter.SetHeldSkel(null);
                        spcInter = null;
                    }
                }
            }
        }
    }
}
