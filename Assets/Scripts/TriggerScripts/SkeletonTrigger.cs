using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class SkeletonTrigger : MonoBehaviour
{
    //Public Variables
    public PlayerBrain.skeleType type;
    public int Form; //This is used in the animator to let it know which form it needs to change to
    public bool isGrabbed = false;
    public string Name;
    public float speed;
    public float jumpHeight;
    public float density;
    public Vector2 colliderSize;
    public Vector2 colliderOffset;
    public CapsuleDirection2D direction;
    public RuntimeAnimatorController controller;
    public Transform skelGObject;
    public Skeleton skeloScript;

    //Private Variables
    ControlMovement cntrlMove;
    BlobController blbCntrl;
    Embodiment embody;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Blob"))
        {
            cntrlMove = collision.GetComponent<ControlMovement>();
            blbCntrl = collision.GetComponent<BlobController>();
            embody = collision.GetComponent<Embodiment>();
            if(cntrlMove != null)
            {
                if (cntrlMove.skeleton == null)
                {
                    cntrlMove.SetEmbodyValues(this);
                }
            }

            if(blbCntrl != null)
            {
                if (blbCntrl.heldSkel == null)
                {
                    blbCntrl.SetHeldSkel(this);
                }
            }

            if(embody != null)
            {
                if(embody.targetSkeleton == null)
                {
                    embody.SetTargetSkeleton(this);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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

        if (blbCntrl != null)
        {
            if (!isGrabbed)
            {
                if (blbCntrl.targetSkeleton == this)
                {
                    blbCntrl.SetHeldSkel(null);
                    blbCntrl = null;
                }
            }
        }

        if (embody != null)
        {
            if (!isGrabbed)
            {
                if (embody.targetSkeleton == this)
                {
                    embody.SetTargetSkeleton(null);
                    embody = null;
                }
            }
        }
    }
}
