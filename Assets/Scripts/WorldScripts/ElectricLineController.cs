using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricLineController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField]
    Texture[] textures;

    int animationStep;

    [SerializeField]
    float fps = 30f;

    float fpsCounter;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        fpsCounter += Time.deltaTime;
        if(fpsCounter >= 1f/fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
                animationStep = 0;

            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0f;
            
        }
    }
}
