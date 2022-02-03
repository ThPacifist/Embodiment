using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public GameObject IndicatorPrefab;
    public GameObject prefabInstance;

    [Space]
    public Controller currentController;

    private void Awake()
    {
        if (PB != null)
            GameObject.Destroy(PB);
        else
            PB = this;
    }
}
