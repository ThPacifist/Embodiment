using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransitionController : MonoBehaviour
{
    public GameAction[] behaviors;
    public static Action slideOutAction = delegate { };
    public static Action slideInAction = delegate { };

    [SerializeField]
    Animator transitionAnimator;

    public bool TriggerSlideIn = false;

    private void OnEnable()
    {
        PlyController.Death += LevelWipe;
    }

    private void OnDisable()
    {
        PlyController.Death -= LevelWipe;
    }

    public void BehaviorTrigger(int num)
    {
        behaviors[num].Action();
    }

    //This function is called by the SlideOut Animation itself by using an animation event
    //Any behavior we want to happen after the screen has been blacked out needs to be subscribed to slideOutAction
    public void SlideOutTrigger()
    {
        TriggerSlideIn = true;
        slideOutAction();
    }
    //This function is called by the SlideOut Animation itself by using an animation event
    //Any behavior we want to happen after the screen has been revealed needs to be subscribed to slideInAction
    public void SlideInTrigger()
    {
        Debug.Log("Slide In Trigger");
        TriggerSlideIn = false;
        slideInAction();
    }

    public void FadeIn() 
    {
        transitionAnimator.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        transitionAnimator.SetTrigger("FadeOut");
    }
    public void SlideIn() 
    {
        transitionAnimator.SetTrigger("SlideIn");
    }
    public void SlideOut()
    {
        transitionAnimator.SetTrigger("SlideOut");
    }

    //This function is used for when we need to hide something the player doesn't need to see, like when the player is respawning
    public void LevelWipe()
    {
        StartCoroutine(LevelWipeCoroutine());    
    }

    IEnumerator LevelWipeCoroutine()
    {
        SlideOut();
        while(!TriggerSlideIn)
        {
            yield return null;
        }
        SlideIn();
    }
}
