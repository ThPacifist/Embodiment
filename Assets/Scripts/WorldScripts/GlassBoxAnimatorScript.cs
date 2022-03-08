using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBoxAnimatorScript : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    public int state;

    private void Start()
    {
        anim.SetInteger("Phase", state);
    }
}
