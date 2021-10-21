using System.Collections;
using System.Collections.Generic;
using CMF;
using UnityEngine;

public class MovingPlatformSequence : MonoBehaviour
{
    private Transform localTransform;
    [SerializeField] private List<Transform> wayPointTransforms;
    [SerializeField] private float rotationSpeed = 1f;
    private float rate;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;



    private GameObject player;

    private bool bSequence = false;
    private int index = 0;

   

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        localTransform = transform;
        MasterPlatformControl.MovingSequence += Movement;
        
    }

    private void OnDisable()
    {
        MasterPlatformControl.MovingSequence -= Movement;
      
    }


    private void Movement(bool incomingBool)
    {
        
        if (index >= 4)
            index = 0;


        if (index == 0 && bSequence == false)
        {

            StartCoroutine(MovementSequencEnumerator1());
        }


        else if (index == 1 && bSequence == false)
            StartCoroutine(RotateSequencEnumerator1());

        else if (index == 2 && bSequence == false)
            StartCoroutine(MovementSequencEnumerator2());

        else if (index == 3 && bSequence == false)
            StartCoroutine(RotateSequencEnumerator2());

    }


    


    IEnumerator MovementSequencEnumerator1()
    {

        bSequence = true;
        float incrementRate = 0;

        while (incrementRate != 1)
        {

            rate += Time.deltaTime;
            Mathf.Clamp(rate, 0f, 1f);
            localTransform.position = Vector3.Lerp(wayPointTransforms[0].position, wayPointTransforms[1].position, rate); //Blend between the waypoints using lerp

            if (localTransform.position == wayPointTransforms[1].position)
                incrementRate = 1;


            


            yield return new WaitForEndOfFrame();
        }


        if (rate >= 1)
        {
            rate = 0;
            index++;
            bSequence = false;
        }




    }

    IEnumerator MovementSequencEnumerator2()
    {

        bSequence = true;
        float incrementRate = 0;

        while (incrementRate != 1)
        {

            rate += Time.deltaTime;
            Mathf.Clamp(rate, 0f, 1f);
            localTransform.position = Vector3.Lerp(wayPointTransforms[1].position, wayPointTransforms[0].position, rate); //Blend between the waypoints using lerp

            if (localTransform.position == wayPointTransforms[0].position)
                incrementRate = 1;


            yield return new WaitForEndOfFrame();
        }




        if (rate >= 1)
        {
            rate = 0;
            index++;
            bSequence = false;
        }
    }

    IEnumerator RotateSequencEnumerator1()
    {
       
        
        bSequence = true;
        float incrementRateR = 0;

        while (incrementRateR < 1)
        {

            if(player.transform.parent != null)
                player.transform.parent = null;


            

            incrementRateR += Time.deltaTime * rotationSpeed;
            incrementRateR = Mathf.Clamp(incrementRateR, 0, 1);
            currentRotation = localTransform.localRotation;
            desiredRotation = Quaternion.Euler(90, 0, 0);
            localTransform.localRotation = Quaternion.Lerp(currentRotation, desiredRotation, incrementRateR);


            

            yield return new WaitForEndOfFrame();
        }




        incrementRateR = 0;
        index++;
        bSequence = false;
    }

    IEnumerator RotateSequencEnumerator2()
    {

       
        bSequence = true;
        float incrementRateR = 0;

        while (incrementRateR < 1)
        {

            if (player.transform.parent != null)
                player.transform.parent = null;

            incrementRateR += Time.deltaTime * rotationSpeed;
            incrementRateR = Mathf.Clamp(incrementRateR, 0, 1);
            currentRotation = localTransform.localRotation;
            desiredRotation = Quaternion.Euler(-90, 0, 0);
            localTransform.localRotation = Quaternion.Lerp(currentRotation, desiredRotation, incrementRateR);


            

            yield return new WaitForEndOfFrame();
        }




        incrementRateR = 0;
        index++;
        bSequence = false;
    }


}
