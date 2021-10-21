using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotation : PlatformAction
{
    

    public List<Transform> targets;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    [SerializeField] private float rotationSpeed = 1f;
    private bool bRotating = false;



    private int flipControl = 0;
    
   


    public override void Action()
    {

        if (bRotating == false)
                StartCoroutine(rotationSequence());
        
    }

    
    IEnumerator rotationSequence()
    {
        
            float incrementRate = 0;

            bRotating = true;


            if (flipControl == 0)
            {

                while (incrementRate != 1)
                {


                    foreach (Transform platform in targets)
                    {

                        incrementRate += Time.deltaTime * rotationSpeed;
                        incrementRate = Mathf.Clamp(incrementRate, 0, 1);
                        currentRotation = platform.localRotation;
                        desiredRotation = Quaternion.Euler(90, 0, 0);
                        platform.localRotation = Quaternion.Lerp(currentRotation, desiredRotation, incrementRate);


                    }
                    yield return new WaitForEndOfFrame();
                }


                bRotating = false;
                flipControl = 1;
                incrementRate = 0;
            }


            else if (flipControl == 1)
            {

                while (incrementRate != 1)
                {


                    foreach (Transform platform in targets)
                    {

                        incrementRate += Time.deltaTime * rotationSpeed;
                        incrementRate = Mathf.Clamp(incrementRate, 0, 1);
                        currentRotation = platform.localRotation;
                        desiredRotation = Quaternion.Euler(-90, 0, 0);
                        platform.localRotation = Quaternion.Lerp(currentRotation, desiredRotation, incrementRate);


                    }
                    yield return new WaitForEndOfFrame();
                }


                bRotating = false;
                flipControl = 0;
                incrementRate = 0;

            }






        


        

    }


}
