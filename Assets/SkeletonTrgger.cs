using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SkeletonInfo
{
    public string Name;
    public float Speed;
    public float JumpHeight;
    public Vector2 colliderSize;
    public Vector2 colliderOffset;
    public CapsuleDirection2D direction;
}

public class SkeletonTrgger : MonoBehaviour
{
    [SerializeField]
    string Name;
    [SerializeField]
    Vector2 colliderSize;
    [SerializeField]
    Vector2 colliderOffset;
    [SerializeField]
    CapsuleDirection2D direction;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    float speed;

    //Private Variables
    ControlMovement cntrlMove;
    SpecialInteractions spcInter;
    SkeletonInfo skeletonData;

    private void Awake()
    {
        skeletonData.Name = Name;
        skeletonData.Speed = speed;
        skeletonData.JumpHeight = jumpHeight;
        skeletonData.colliderSize = colliderSize;
        skeletonData.colliderOffset = colliderOffset;
        skeletonData.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Blob"))
        {
            cntrlMove = collision.GetComponent<ControlMovement>();
            spcInter = collision.GetComponent<SpecialInteractions>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
