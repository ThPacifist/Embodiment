using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class AnimatedTalkBubble : MonoBehaviour
{
    public Canvas bubbleCanvas;
    public GameObject bubbleTail;
    public GameObject talkBubble;
    public GameObject Expoyzte;
    public Vector3 target;

    private void Update()
    {
        target = Camera.main.WorldToScreenPoint(bubbleCanvas.transform.position);

        float adjustment = 5f;

        if(target.x < adjustment && target.y < adjustment && target.x > (Camera.main.pixelWidth - adjustment) && target.y > (Camera.main.pixelHeight - adjustment))
        {
            Debug.Log("Inside Check");
            bubbleCanvas.transform.parent = null;
        }
        else
        {
            Debug.Log("Inside else");
            bubbleCanvas.transform.parent = Expoyzte.transform;
        }
    }
}
