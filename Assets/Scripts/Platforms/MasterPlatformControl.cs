using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MasterPlatformControl : MonoBehaviour
{
    // PLATFORMMMMM PEEEETTTTTEEEEEEEE
   [SerializeField] private bool bEnableMovingSequence = true;
    public static Action<bool> MovingSequence = delegate { };
    private float timer = 0f;

    public List<PlatformAction> actions;

    private void Update()
    {
      
        timer += Time.deltaTime;
        timer = Mathf.Clamp(timer, 0f, 4f);
        if (bEnableMovingSequence == true && timer >= 4f)
        {
            StartCoroutine(masterTimer());
            timer = 0f;
        }
            
    }
    IEnumerator masterTimer ()
    {
        
        yield return null;
        foreach (PlatformAction item in actions)
            item.Action();
        MovingSequence(bEnableMovingSequence);
        
    }
}
