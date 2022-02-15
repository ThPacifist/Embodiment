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
                ControlMovement.canDisembody = false;
                plyCntrl.treadmill = true;
                treadmill.SetPlyCntrl(plyCntrl);
                collision.transform.position = lockPos.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (plyCntrl != null)
        {
            ControlMovement.canDisembody = true;
            plyCntrl.treadmill = false;
            treadmill.SetPlyCntrl(null);
        }
    }
}
