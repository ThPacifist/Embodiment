using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAssistant : MonoBehaviour
{
    public string PlayOnStart;
    public AudioSource currentPlayingTrack;

    public List<Sound> music = new List<Sound>();

    [HideInInspector]
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        bool check;
        if (PlayOnStart != "")
        {
            check = Play(PlayOnStart);

            if (!check)
            {
                int rand = Random.Range(0, music.Count);
                Play(music[rand].name);
            }
        }
        else
        {
            int rand = Random.Range(0, music.Count);
            Play(music[rand].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool Play(string sound)
    {
        Sound s = music.Find(item => item.name == sound);
        if(s == null)
        {
            Debug.LogError("Music not Found");
            return false;
        }

        if (currentPlayingTrack != null && currentPlayingTrack.isPlaying)
        {
            StartCoroutine(FadeTo(sound));
        }
        else
        {
            s.source.Play();
            currentPlayingTrack = s.source;
        }

        return true;
    }

    //Stops the current playing track
    void Stop()
    {
        if (currentPlayingTrack != null)
        {
            currentPlayingTrack.Stop();
            currentPlayingTrack = null;
        }
    }

    public IEnumerator FadeTo(string to)
    {
        Sound s = music.Find(item => item.name == to);
        if (s == null)
        {
            Debug.LogError("Music track not found");
            yield break;
        }
        else
        {
            float currentTime = 0;
            float duration = 2;
            float start = currentPlayingTrack.volume;
            s.source.volume = 0;
            audioManager.Play(s.name);

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                s.source.volume = Mathf.Lerp(0, s.volume, currentTime / duration);
                currentPlayingTrack.volume = Mathf.Lerp(start, 0, currentTime / duration);
                yield return null;
            }

            audioManager.Stop(currentPlayingTrack.name);
            currentPlayingTrack = s.source;
        }
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
