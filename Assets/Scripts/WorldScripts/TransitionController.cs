using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransitionController : MonoBehaviour
{
    public GameAction[] behaviors;
    public static Action slideOutAction = delegate { };
    public static Action slideInAction = delegate { };
    public static Action fadeOutAction = delegate { };

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
    void SlideOutTrigger()
    {
        TriggerSlideIn = true;
        slideOutAction();
    }
    //This function is called by the SlideOut Animation itself by using an animation event
    //Any behavior we want to happen after the screen has been revealed needs to be subscribed to slideInAction
    void SlideInTrigger()
    {
        TriggerSlideIn = false;
        slideInAction();
    }
    //This function is called by the FadeOut Animation itself by using an animation event
    //Any behavior we want to happen after the screen has been blacked out needs to be subscribed to fadeOutAction
    //Note: this functions should only be used stuff like changing scenes
    void FadeOutTrigger()
    {
        fadeOutAction();
    }

    //These functions call there respective animation
    //Use them to trigger the animations
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
