using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratchable : MonoBehaviour
{
    [SerializeField]
    GameAction behavior;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerAttack"))
        {
            CatController.Scratch += behavior.Action;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            CatController.Scratch -= behavior.Action;
        }
    }
}
