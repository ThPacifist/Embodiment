using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    [SerializeField]
    Transform gObject;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    Transform endPos;

    public PlyController plyCntrl;
    Vector3 restPos;

    private void Awake()
    {
        restPos = gObject.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (plyCntrl != null)
        {
            if (plyCntrl.Left)
            {
                UpdateGameObject('+');
            }
            else if (plyCntrl.Right)
            {
                UpdateGameObject('-');
            }
        }
    }

    void UpdateGameObject(char value)
    {
        if(value == '+')
        {
            if (Vector2.Distance(gObject.position, endPos.position) > 0.001)
            {
                gObject.position = Vector2.MoveTowards(gObject.position, endPos.position, Time.deltaTime * speed);
            }
        }
        else if(value == '-')
        {
            if (Vector2.Distance(gObject.position, restPos) > 0.001)
            {
                gObject.position = Vector2.MoveTowards(gObject.position, restPos, Time.deltaTime * speed);
            }
        }
    }
}
