using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
    public GameObject cutsceneGameobject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cutsceneGameobject.SetActive(true);
    }
}
