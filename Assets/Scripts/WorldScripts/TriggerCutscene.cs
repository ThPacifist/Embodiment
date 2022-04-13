using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
    public GameObject cutsceneGameobject;

    private void Awake()
    {
        cutsceneGameobject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBrain.PB.canJump = false;
        PlayerBrain.PB.canMove = false;
        cutsceneGameobject.SetActive(true);
    }

    public void UnFreezePlayer()
    {
        PlayerBrain.PB.canJump = true;
        PlayerBrain.PB.canMove = true;
        gameObject.SetActive(false);
    }
}
