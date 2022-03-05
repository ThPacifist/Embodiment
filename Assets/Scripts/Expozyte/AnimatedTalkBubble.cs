using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[ExecuteAlways]
public class AnimatedTalkBubble : MonoBehaviour
{
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

        bubbleCanvas.GetWorldCorners(corners);
        //corners[0] is bot left
        //corners[1] is top left
        //corners[2] is top right
        //corners[3] is bot right

        //Constraints for Canvas
        bubbleCanvas.transform.position = Vector3.MoveTowards(bubbleCanvas.transform.position, 
            Expoyzte.transform.position + new Vector3(0, -1.5f, 0), 1);

        Vector3 camTR = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        Vector3 camBL = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        wordBubble.GetWorldCorners(corners);
        Vector3 midpoint = (corners[0] + corners[2]) / 2;
        float xDist = Mathf.Abs(midpoint.x - corners[0].x);
        float yDist = Mathf.Abs(midpoint.y - corners[0].y);

        bubbleCanvas.transform.position = new Vector3(
            Mathf.Clamp(bubbleCanvas.transform.position.x, camBL.x + xDist, camTR.x - xDist),
            Mathf.Clamp(bubbleCanvas.transform.position.y, camBL.y + yDist, camTR.y - yDist),
            0);

        //Constraints for bubble tail
        Vector2 targetDir = Expoyzte.transform.position - bubbleTail.transform.position;
        float signedAngle = Vector2.SignedAngle(targetDir, Vector2.down);
        Quaternion rotation = Quaternion.Euler(0, 0, -signedAngle);
        bubbleTail.transform.rotation = rotation;

        bubbleTail.transform.position = new Vector3(
            Mathf.Clamp(Expoyzte.transform.position.x, corners[0].x + 0.5f, corners[2].x - 0.5f),
            Mathf.Clamp(Expoyzte.transform.position.y, corners[0].y + 0.4f, corners[2].y - 0.4f),
            0);

    }
}
