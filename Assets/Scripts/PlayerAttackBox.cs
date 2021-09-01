using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    //Code added by Jason on 9/1

    //Public variables and assets
    public Transform hitbox;

    //Private variables
    private float timeElapsed;

    //Runs the timer
    private void Update()
    {
        StartCoroutine("DeleteBox");
    }

    //Timer before it disappears
    IEnumerator DeleteBox()
    {
        yield return new WaitForEndOfFrame();
        timeElapsed += Time.deltaTime;
        if(timeElapsed > 1)
        {
            Destroy(hitbox.gameObject);
        }
    }
}
