using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotationDelayed : PlatformAction
{
    

    public List<Transform> choosePlatforms;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    [SerializeField] private float rotationSpeed = 1f;
    private bool bRotating = false;



    private int flipControl = 0;
    public override void Action()
    {
        if (bRotating == false)
            StartCoroutine(rotationSequenceDelay());

        //Platforms here move with a delay, cool staircase effect

    }

    IEnumerator rotationSequenceDelay()
    {

        bRotating = true;


        if (flipControl == 0)
        {

            foreach (Transform platform in choosePlatforms)
            {
                float incrementRate = 0;

                while (incrementRate != 1)
                {

                    yield return new WaitForEndOfFrame();
                    incrementRate += Time.deltaTime * rotationSpeed;
                    incrementRate = Mathf.Clamp(incrementRate, 0, 1);
                    currentRotation = platform.localRotation;
                    desiredRotation = Quaternion.Euler(90, 0, 0);
                    platform.localRotation = Quaternion.Lerp(currentRotation, desiredRotation, incrementRate);
                }

            }
            bRotating = false;
            flipControl = 1;
        }
        else if (flipControl == 1)
        {

            foreach (Transform platform in choosePlatforms)
            {
                float incrementRate = 0;


                while (incrementRate != 1)
                {

                    yield return new WaitForEndOfFrame();
                    incrementRate += Time.deltaTime * rotationSpeed;
                    incrementRate = Mathf.Clamp(incrementRate, 0, 1);
                    currentRotation = platform.localRotation;
                    desiredRotation = Quaternion.Euler(-90, 0, 0);
                    platform.localRotation = Quaternion.Lerp(currentRotation, desiredRotation, incrementRate);
                }

            }
            bRotating = false;
            flipControl = 0;

        }


    }


}
