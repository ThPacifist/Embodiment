using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    //Variables and assets
    public Transform skeleton;
    public Transform spawnPoint;
    public bool changeLocationAtStart = false;

    //If you want it to change locations at the start, use it
    private void Start()
    {
        if(changeLocationAtStart)
        {
            skeleton.position = spawnPoint.position;
        }
    }

    //Check if it has entered a killzone
    private void OnTriggerEnter2d(Collider other)
    {
        if(other.CompareTag("Death"))
        {
            skeleton.position = spawnPoint.position;
        }
    }
}
