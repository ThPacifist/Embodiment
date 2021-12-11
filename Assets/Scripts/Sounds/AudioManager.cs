using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance; //Is there an audio manager? Used to ensure only one instance

	[SerializeField]
	bool isTutorial = false;

	public AudioMixerGroup mixerGroup; //For audio source mixing

	public Sound[] sounds; //List of sounds managed by the manager

    public List<Sound> fadingSounds = new List<Sound>();

	void Awake()
	{
		if (!isTutorial)
		{
			if (instance != null && instance != null)
			{
				Destroy(gameObject); //Is there a manager? If yes then I'm gone
			}
			else
			{
				instance = this;  //There isnt a manager? I'm it
				DontDestroyOnLoad(gameObject);
			}
		}

        foreach (Sound s in sounds) //Init each sound - give it a source and init that source to make it playable
        {
            GameObject gObject = new GameObject();
            s.source = gObject.AddComponent<AudioSource>();
            gObject.name = s.clip.name;
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;

            gObject.transform.parent = this.transform;

            s.source.outputAudioMixerGroup = mixerGroup;
        }
	}

    void Update()
    {
        //doFade();
    }

    public void Play(string sound, bool isRandom) 
	{
		Sound s = Array.Find(sounds, item => item.name == sound); //Find the sound we want to play, ensure it's not null
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}


		if (!s.source.isPlaying) //Check if the sound is playing - if not, have at it.s
		{
            if (!isRandom)
            {
                s.source.volume = s.volume; //Set the volume and pitch to the one contained by the sound

                s.source.pitch = s.pitch;
            }

            else if (isRandom)
            {
                s.source.volume = Random.Range(s.volume - 0.05f, s.volume + 0.05f); //Random Volume

                s.source.pitch = Random.Range(0.9f, 1.1f); // Random Pitch
            }

            s.source.Play();

		}
	}

    public void PlayAnyway(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound); //Find the sound we want to play, ensure it's not null
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound); //Find the sound we want to play, ensure it's not null
        if(s == null)
        {
            Debug.Log("S: " + sound + " is missing");
            return;
        }

        if (s.source != null && s.source.isPlaying)
        {
            s.source.Stop();
        }
    }

    public void StartFade(string sound, float ttq)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.setFadeTime(ttq);

        fadingSounds.Add(s);

    }
    public void doFade()
    {
        if (fadingSounds.Count != 0)
        {
            List<Sound> toremove = new List<Sound>();
            foreach (Sound s in fadingSounds)
            {
                bool remove = s.AudioFadeOut();
                if (remove)
                {
                    toremove.Add(s);
                }
            }
            if(toremove.Count != 0)
            {
                foreach(Sound s in toremove)
                {
                    fadingSounds.Remove(s);
                }
                toremove.Clear();
            }
        }
    }

    public void recreateSounds()
    {
        //First, run through and destory each instance of audio source
        Component[] sources = this.gameObject.GetComponents<AudioSource>() as Component[];
        foreach(Component source in sources)
        {
            Destroy(source as AudioSource);
        }

        foreach (Sound s in sounds) //Then recreate it
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    public void StopAll()
    {
        foreach(Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public void updateVolume(float val)
    {
        foreach(Sound s in sounds)
        {
            s.volume = val;
        }
    }
}
