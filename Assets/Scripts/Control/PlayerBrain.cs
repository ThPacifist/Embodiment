using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

/// <summary>
/// This holds all the variables that are used the most that by the player
/// </summary>
public class PlayerBrain : MonoBehaviour
{
    //Allows for an easy way of accessing the different forms
    public enum skeleType
    {
        Blob,
        Cat,
        Fish,
        Bat,
        Human
    }

    /// <summary>
    /// Reference this to have access to this scripts field without needing an object reference in every script
    /// </summary>
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
    public Tilemap tileMap;
    public bool canMove = true;
    public bool canJump = true;
    public bool inWater = false;

    /// <summary>
    /// Represents the current active controller, in other words, the current form
    /// </summary>
    [Space]
    public Controller currentController;

    /// <summary>
    /// Dictionary for all skeleton controllers
    /// </summary>
    public static Dictionary<skeleType, Controller> Skeletons;

    private void Awake()
    {
        //Makes sure there is only one instance of Player brain
        if (PB != null)
            GameObject.Destroy(PB);
        else
            PB = this;

        //Builds the Dictionary to be used by scripts
        //Should happen before all scripts to make sure it gets made
        Skeletons = new Dictionary<skeleType, Controller>();
        Skeletons.Add(skeleType.Blob, GetComponent<BlobController>());
        Skeletons.Add(skeleType.Cat, GetComponent<CatController>());
    }
}
