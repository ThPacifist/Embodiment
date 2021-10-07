using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxUpdater : MonoBehaviour
{
    /*public Rigidbody2D[] lightB;
    public Rigidbody2D[] mediumB;
    public Rigidbody2D[] heavyB;*/

    public PlyController plyCntrl;
    public Rigidbody2D Rb;
    public GameObject player;

    private void OnEnable()
    {
        PlyController.Embody += CheckPlayer;
    }

    private void OnDisable()
    {
        PlyController.Embody -= CheckPlayer;
    }

    void CheckPlayer()
    {
        if(player.CompareTag("Blob"))
        {

        }
        else if(player.CompareTag("Fish"))
        {

        }
        else if (player.CompareTag("Cat"))
        {

        }
        else if (player.CompareTag("Human"))
        {

        }
        else if (player.CompareTag("Bat"))
        {

        }
    }

    void ChangeMass()
    {

    }
}
