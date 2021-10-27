using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public GameAction[] behaviors;
    [SerializeField]
    Animator transitionAnimator;

    public bool TriggerSlideIn = false;
    public void BehaviorTrigger(int num)
    {
        behaviors[num].Action();
    }

    public void SlideOutTrigger()
    {
        TriggerSlideIn = true;
    }
    public void SlideInTrigger()
    {
        TriggerSlideIn = false;
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
        Debug.Log("Slide in was triggered");
        transitionAnimator.SetTrigger("SlideIn");
    }
    public void SlideOut()
    {
        transitionAnimator.SetTrigger("SlideOut");
    }
}
