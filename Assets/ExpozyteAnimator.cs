using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpozyteAnimator : MonoBehaviour
{
    public Animator expAnim;
    public GameObject Player;
    public float angle;

    private void Update()
    {
        Vector2 direction = transform.position - Player.transform.position;

        angle = Vector2.SignedAngle(Vector2.up, direction);
    }
}
