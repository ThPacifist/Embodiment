using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
    public PlayableDirector director;
    public PlayableAsset playable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        director.Play(playable);
    }
}
