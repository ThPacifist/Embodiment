using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillTrigger : MonoBehaviour
{
    public Transform lockPos;

    PlyController plyCntrl;

    [SerializeField]
    Treadmill treadmill;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Cat"))
        {
            plyCntrl = collision.GetComponent<PlyController>();
            if(plyCntrl != null)
            {
                ControlMovement.canEmbody = false;
                ControlMovement.canDisembody = false;
                plyCntrl.move = false;
                treadmill.plyCntrl = plyCntrl;
                collision.transform.position = lockPos.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (plyCntrl != null)
        {
            ControlMovement.canEmbody = true;
            ControlMovement.canDisembody = true;
            plyCntrl.move = true;
            treadmill.plyCntrl = null;
        }
    }
}
