using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAssistant : MonoBehaviour
{
    public string PlayOnStart;
    [SerializeField]
    public Sound currentPlayingTrack;

    public List<Sound> music = new List<Sound>();

    [HideInInspector]
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        Sound currentPlayingTrack = music.Find(item => item.name == PlayOnStart);
        if (currentPlayingTrack == null)
        {
            Debug.LogError("Music Track not Found");
        }
        else
        {
            Play(PlayOnStart);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string sound)
    {
        Sound s = music.Find(item => item.name == sound);
        if(s == null)
        {
            Debug.LogError("Music not Found");
            return;
        }

        if(currentPlayingTrack.source.isPlaying)
        {
            return;
        }
        else
        s.source.Play();
    }

    public void FadeTo(string to)
    {

    }
}
