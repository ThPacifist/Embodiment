using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : GameAction
{
    public float EndAngle;
    public float rotationSpeed = 1;

    [SerializeField]
    GameObject effectedObject;

    Quaternion rotation;

    public override void Action()
    {
        
    }

    public override void Action(bool b)
    {
        
    }

    // Start is called before the first frame update
    void Awake()
    {
        rotation = Quaternion.Euler(0, 0, EndAngle);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3.RotateTowards(effectedObject.transform.position, new Vector3(0, 0, EndAngle), (rotationSpeed * Time.deltaTime),
            0.0f);
    }
}
