using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : GameAction
{
    public float EndAngle;
    public float rotationSpeed = 1;

    [SerializeField]
    GameObject effectedObject;

    Quaternion restRotation;
    Quaternion endRotation;

    public override void Action()
    {
        
    }

    public override void Action(bool b)
    {
        
    }

    // Start is called before the first frame update
    void Awake()
    {
        restRotation = effectedObject.transform.rotation;
        endRotation = Quaternion.Euler(new Vector3(0, 0, EndAngle));
    }

    // Update is called once per frame
    void Update()
    {


        //Vector2.SignedAngle(Vector2.up, )

        effectedObject.transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
    }
}
