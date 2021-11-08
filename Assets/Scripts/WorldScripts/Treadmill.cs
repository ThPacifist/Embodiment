using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    [SerializeField]
    float completion = 0;
    [SerializeField]
    float step = 1;

    public PlyController plyCntrl;

    // Update is called once per frame
    void Update()
    {
        /*
         * if player moves left, update completion float
         */
        if (plyCntrl != null)
        {
            if (plyCntrl.Left)
            {
                completion += Time.deltaTime * step;
            }
            else if (plyCntrl.Right)
            {
                completion -= Time.deltaTime * step;
            }
        }

        if(completion >= 100)
        {
            plyCntrl.move = true;
        }
    }
}
