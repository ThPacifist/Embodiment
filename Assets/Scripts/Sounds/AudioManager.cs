using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance; //Is there an audio manager? Used to ensure only one instance

	public AudioMixerGroup mixerGroup; //For audio source mixing

    public Sound[] Music; //List of music tracks
	public Sound[] sounds; //List of sounds managed by the manager

    GameObject mAssistGO;
    MusicAssistant mAssist;

    GameObject sAssistGO;
    SoundAssistant sAssist;

	void Awake()
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


        CreateAssistants();
	}

    void Update()
    {
        //doFade();
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound); //Find the sound we want to play, ensure it's not null
        if (s == null)
        {
            s = Array.Find(Music, item => item.name == sound); //If it's not a sound effect, see if it's a music track

            if (s == null)
            {
                Debug.LogWarning("Sound and/or Music: " + name + " not found!");
                return;
            }
        }

        s.source.Play();
    }

	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound); //Find the sound we want to play, ensure it's not null
        if(s == null)
        {
            s = Array.Find(Music, item => item.name == sound); //If it's not a sound effect, see if it's a music track

            if (s == null)
            {
                Debug.LogWarning("Sound and/or Music: " + name + " is music");
                return;
            }
        }

        if (s.source != null && s.source.isPlaying)
        {
            s.source.Stop();
        }
    }

    [ContextMenu ("Create Assistants")]
    void CreateAssistants()
    {
        if(mAssistGO != null)
        {
            DestroyImmediate(mAssistGO);
        }
        mAssistGO = new GameObject();
        mAssistGO.name = "Music Assistant";
        mAssistGO.transform.parent = this.transform;
        mAssist = mAssistGO.AddComponent<MusicAssistant>();
        mAssist.audioManager = this;
        foreach (Sound s in Music)
        {
            GameObject gObject = new GameObject();
            s.source = gObject.AddComponent<AudioSource>();
            gObject.name = s.clip.name;
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;

            gObject.transform.parent = mAssistGO.transform;

            s.source.outputAudioMixerGroup = mixerGroup;
            mAssist.music.Add(s);
        }

        if (sAssistGO != null)
        {
            DestroyImmediate(sAssistGO);
        }
        sAssistGO = new GameObject();
        sAssistGO.name = "Sound Assistant";
        sAssistGO.transform.parent = this.transform;
        sAssist = sAssistGO.AddComponent<SoundAssistant>();
        sAssist.audioManager = this;
        foreach (Sound s in sounds) //Init each sound - give it a source and init that source to make it playable
        {
            GameObject gObject = new GameObject();
            s.source = gObject.AddComponent<AudioSource>();
            gObject.name = s.clip.name;
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;

            gObject.transform.parent = sAssistGO.transform;

            s.source.outputAudioMixerGroup = mixerGroup;
            sAssist.soundEffects.Add(s);
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
