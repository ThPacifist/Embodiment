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
            ChangeMass("Blob");
        }
        else if(player.CompareTag("Fish"))
        {
            ChangeMass("Fish");
        }
        else if (player.CompareTag("Cat"))
        {
            ChangeMass("Cat");
        }
        else if (player.CompareTag("Human"))
        {
            ChangeMass("Human");
        }
        else if (player.CompareTag("Bat"))
        {
            ChangeMass("Bat");
        }
    }

    void ChangeMass(string tag)
    {
        if(tag == "Blob")
        {
            if(this.CompareTag("LBox"))
            {
                Rb.mass = 1;
            }
            else if(this.CompareTag("MBox"))
            {
                Rb.mass = 30;
            }
            else if(this.CompareTag("HBox"))
            {
                Rb.mass = 80;
            }
        }
        else if (tag == "Human")
        {
            if (this.CompareTag("LBox"))
            {
                Rb.mass = 1;
            }
            else if (this.CompareTag("MBox"))
            {
                Rb.mass = 70;
            }
            else if (this.CompareTag("HBox"))
            {
                Rb.mass = 160;
            }
        }
        else if (tag == "Cat")
        {
            if (this.CompareTag("LBox"))
            {
                Rb.mass = 10;
            }
            else if (this.CompareTag("MBox"))
            {
                Rb.mass = 250;
            }
            else if (this.CompareTag("HBox"))
            {
                Rb.mass = 300;
            }
        }
        else if (tag == "Fish")
        {
            if (this.CompareTag("LBox"))
            {
                Rb.mass = 1;
            }
            else if (this.CompareTag("MBox"))
            {
                Rb.mass = 30;
            }
            else if (this.CompareTag("HBox"))
            {
                Rb.mass = 80;
            }
        }
        else if (tag == "Bat")
        {
            if (this.CompareTag("LBox"))
            {
                Rb.mass = 1;
            }
            else if (this.CompareTag("MBox"))
            {
                Rb.mass = 80;
            }
            else if (this.CompareTag("HBox"))
            {
                Rb.mass = 80;
            }
        }
    }
}
