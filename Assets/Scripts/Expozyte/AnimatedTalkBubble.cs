using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[ExecuteAlways]
public class AnimatedTalkBubble : MonoBehaviour
{
    public float xBounds;
    public float yBounds;
    public float xBubbleBounds;
    public float yBubbleBounds;

    public RectTransform bubbleCanvas;
    public RectTransform bubbleTail;
    public RectTransform wordBubble;
    public GameObject Expoyzte;
    public Camera cam;
    CinemachineBrain camBrain;
    CinemachineVirtualCamera virCam;

    private void Update()
    {
        Vector3 expoyzteScreenPos = cam.WorldToScreenPoint(Expoyzte.transform.position);

        Vector3[] corners = new Vector3[4];
        Vector3[] wordBubbleCorners = new Vector3[4];

        bubbleCanvas.GetWorldCorners(corners);
        //corners[0] is bot left
        //corners[1] is top left
        //corners[2] is top right
        //corners[3] is bot right
        wordBubble.GetWorldCorners(wordBubbleCorners);

        //Constraints for Canvas
        bubbleCanvas.transform.position = Vector3.MoveTowards(bubbleCanvas.transform.position, 
            Expoyzte.transform.position + new Vector3(0, -1.5f, 0), 1);

        Vector3 camTR = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        Vector3 camBL = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        //bubbleCanvas.GetWorldCorners(corners);
        Vector3 midpoint = (wordBubbleCorners[0] + wordBubbleCorners[2]) / 2;
        float xDist = Mathf.Abs(wordBubbleCorners[0].x - midpoint.x);
        float yDist = Mathf.Abs(wordBubbleCorners[0].y - midpoint.y);

        bubbleCanvas.transform.position = new Vector3(
            Mathf.Clamp(bubbleCanvas.transform.position.x, camBL.x + xDist + xBounds, camTR.x - xDist - xBounds),
            Mathf.Clamp(bubbleCanvas.transform.position.y, camBL.y + yDist + yBounds, camTR.y - yDist - yBounds),
            0);

        //Constraints for bubble tail
        Vector2 targetDir = Expoyzte.transform.position - bubbleTail.transform.position;
        float signedAngle = Vector2.SignedAngle(targetDir, Vector2.down);
        Quaternion rotation = Quaternion.Euler(0, 0, -signedAngle);
        bubbleTail.transform.rotation = rotation;

        bubbleTail.transform.position = new Vector3(
            Mathf.Clamp(Expoyzte.transform.position.x, wordBubbleCorners[0].x + xBubbleBounds, wordBubbleCorners[2].x - xBubbleBounds),
            Mathf.Clamp(Expoyzte.transform.position.y, wordBubbleCorners[0].y + yBubbleBounds, wordBubbleCorners[2].y - yBubbleBounds),
            0);

    }
}
