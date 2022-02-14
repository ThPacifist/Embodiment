using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillTrigger : MonoBehaviour
{
    public Transform lockPos;

    CatController cat;

    [SerializeField]
    Treadmill treadmill;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Cat"))
        {
            cat = collision.GetComponent<CatController>();
            if(cat != null)
            {
                //ControlMovement.canEmbody = false;
                Embodiment.canDisembody = false;
                cat.treadmill = true;
                treadmill.SetCatCntrl(cat);
                collision.transform.position = lockPos.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (cat != null)
        {
            ControlMovement.canEmbody = true;
            ControlMovement.canDisembody = true;
            cat.treadmill = false;
            treadmill.SetCatCntrl(null);
        }
    }
}
