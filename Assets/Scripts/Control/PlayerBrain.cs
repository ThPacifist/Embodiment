using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
    public enum skeleType
    {
        Blob,
        Cat,
        Fish,
        Bat,
        Human
    }

    public static PlayerBrain PB;

    public CapsuleCollider2D plyCol;
    public Animator plyAnim;
    public Rigidbody2D rb;
    public SpriteRenderer plySpr;
    public ControlMovement CM;
    public FixedJoint2D fixedJ;
    public SpringJoint2D spring;
    public LayerMask groundLayerMask;
    public GameObject IndicatorPrefab;
    public GameObject prefabInstance;

    [Space]
    public Controller currentController;

    //Dictionary for skeletons
    public static Dictionary<skeleType, Type> Skeletons = new Dictionary<skeleType, Type>()
    { 
        {skeleType.Blob, typeof(BlobController)},
    };

    private void Awake()
    {
        if (PB != null)
            GameObject.Destroy(PB);
        else
            PB = this;
    }
}
