using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    public static PlayerBrain PB;

    public CapsuleCollider2D plyCol;
    public Animator plyAnim;
    public Rigidbody2D rb;
    public SpriteRenderer plySpr;
    public ControlMovement CM;
    public FixedJoint2D fixedJ;
    public SpringJoint2D spring;

    private void Awake()
    {
        if (PB != null)
            GameObject.Destroy(PB);
        else
            PB = this;
    }
}
