using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public GameAction[] behaviors;
    [SerializeField]
    Animator transitionAnimator;

    public void BehaviorTrigger(int num)
    {
        behaviors[num].Action();
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
}
