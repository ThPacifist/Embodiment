using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class AnimatedTalkBubble : MonoBehaviour
{
    public RectTransform bubbleCanvas;
    public RectTransform bubbleTail;
    public RectTransform wordBubble;
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

        //Constraints for Canvas
        bubbleCanvas.transform.position = Vector3.MoveTowards(bubbleCanvas.transform.position, 
            Expoyzte.transform.position + new Vector3(0, -1.5f, 0), 1);

        bubbleCanvas.transform.position = new Vector3(
            Mathf.Clamp(bubbleCanvas.transform.position.x, cam.transform.position.x - 6, cam.transform.position.x + 6),
            Mathf.Clamp(bubbleCanvas.transform.position.y, cam.transform.position.y - 3.5f, cam.transform.position.y + 3.5f),
            0);

        //Constraints for bubble tail
        Vector2 targetDir = Expoyzte.transform.position - bubbleTail.transform.position;
        float signedAngle = Vector2.SignedAngle(targetDir, Vector2.down);
        Quaternion rotation = Quaternion.Euler(0, 0, -signedAngle);
        bubbleTail.transform.rotation = rotation;

        wordBubble.GetWorldCorners(corners);
        bubbleTail.transform.position = new Vector3(
            Mathf.Clamp(Expoyzte.transform.position.x, corners[0].x + 0.5f, corners[2].x - 0.5f),
            Mathf.Clamp(Expoyzte.transform.position.y, corners[0].y + 0.4f, corners[2].y - 0.4f),
            0);

    }
}
