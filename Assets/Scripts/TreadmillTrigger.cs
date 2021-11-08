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
            Debug.Log("Cat is inside trigger");
            plyCntrl = collision.GetComponent<PlyController>();
            if(plyCntrl != null)
            {
                plyCntrl.player = lockPos;
                plyCntrl.move = false;
                treadmill.plyCntrl = plyCntrl;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (plyCntrl != null)
        {
            plyCntrl.move = true;
            treadmill.plyCntrl = null;
        }
    }
}
