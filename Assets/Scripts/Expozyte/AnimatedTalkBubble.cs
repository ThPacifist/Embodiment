using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteAlways]
public class AnimatedTalkBubble : MonoBehaviour
{
    public Canvas bubbleCanvas;
    public GameObject bubbleTail;
    public GameObject talkBubble;
    public Vector3 target;

    private void Update()
    {
        target = Camera.main.WorldToScreenPoint(bubbleCanvas.transform.position);
    }
}
