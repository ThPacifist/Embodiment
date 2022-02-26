using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class AnimatedTalkBubble : MonoBehaviour
{
    public RectTransform bubbleCanvas;
    public GameObject bubbleTail;
    public GameObject talkBubble;
    public GameObject Expoyzte;
    public Camera cam;

    private void Update()
    {
        Vector3[] corners = new Vector3[4];

        bubbleCanvas.GetWorldCorners(corners);
        //corners[0] is bot left
        //corners[1] is top left
        //corners[2] is top right
        //corners[3] is bot right

        /*
         * bubbleCavas.transform.position = new Vector3( Mathf.Clamp(bubbleCanvas.transform.position.x ,min,max),       * 
         */

        bubbleCanvas.transform.position = Vector3.MoveTowards(bubbleCanvas.transform.position, 
            Expoyzte.transform.position + new Vector3(0, -1.5f, 0), 1);

        bubbleCanvas.transform.position = new Vector3(
            Mathf.Clamp(bubbleCanvas.transform.position.x, cam.transform.position.x - 6, cam.transform.position.x + 6),
            Mathf.Clamp(bubbleCanvas.transform.position.y, cam.transform.position.y - 3.5f, cam.transform.position.y + 3.5f),
            0);


    }
}
