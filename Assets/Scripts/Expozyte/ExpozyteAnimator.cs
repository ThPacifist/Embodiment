using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpozyteAnimator : MonoBehaviour
{
    public Animator expAnim;
    public float angle;
    public float distance;

    private void Update()
    {
        distance = Vector3.Distance(transform.position, PlayerBrain.PB.transform.position);
        Vector2 direction = transform.position - PlayerBrain.PB.transform.position;

        angle = Vector2.SignedAngle(Vector2.up, direction);

        if (distance > 0.5f)
        {
            if ((angle >= 0 && angle < 22.5) || (angle <= 0 && angle > -22.5)) //Player is below the expozyte
            {
                //Debug.Log("The Blob is below");
                expAnim.SetInteger("Direction", 8);
            }
            else if (angle <= -22.5 && angle > -67.5)
            {
                //Debug.Log("The Blob is bottom left");
                expAnim.SetInteger("Direction", 7);
            }
            else if (angle <= -67.5 && angle > -112.5)
            {
                //Debug.Log("The Blob is to the left");
                expAnim.SetInteger("Direction", 4);
            }
            else if (angle <= -112.5 && angle < -157.5)
            {
                //Debug.Log("The Blob is top Left");
                expAnim.SetInteger("Direction", 1);
            }
            else if ((angle >= -157.5 && angle <= -180) || (angle >= 157.5 && angle <= 180))
            {
                //Debug.Log("The Blob is above");
                expAnim.SetInteger("Direction", 2);
            }
            else if (angle >= 112.5 && angle < 157.5)
            {
                //Debug.Log("The Blob is top right");
                expAnim.SetInteger("Direction", 3);
            }
            else if (angle >= 67.5 && angle < 112.5)
            {
                //Debug.Log("The Blob is to the right");
                expAnim.SetInteger("Direction", 6);
            }
            else if (angle >= 22.5 && angle < 67.5)
            {
                //Debug.Log("The Blob is bottom right");
                expAnim.SetInteger("Direction", 9);
            }
        }
        else
        {
            //Debug.Log("The Expozyte is looking forward");
            expAnim.SetInteger("Direction", 5);
        }
    }
}
