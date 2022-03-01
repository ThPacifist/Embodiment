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

    public GameObject mAssistGO;
    MusicAssistant mAssist;

    public GameObject sAssistGO;
    SoundAssistant sAssist;

    public Sound[] Music; //List of music tracks
	public Sound[] sounds; //List of sounds managed by the manager

    bool isFading = false;

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

        //CreateAssistants();
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

        if(!s.source.isPlaying)
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
            if (s.source.volume != s.volume)
                s.source.volume = s.volume;
            s.source.Stop();
        }
    }

    public void CreateAssistants()
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
        mAssist.mixerGroup = mixerGroup.audioMixer.FindMatchingGroups("Music")[0];
        if (Music.Length > 0)
        {
            foreach (Sound s in Music)
            {
                GameObject gObject = new GameObject();
                s.source = gObject.AddComponent<AudioSource>();
                gObject.name = s.clip.name;
                s.source.clip = s.clip;
                s.source.loop = true;
                s.source.volume = s.volume;
                s.source.playOnAwake = false;

                gObject.transform.parent = mAssistGO.transform;

                s.source.outputAudioMixerGroup = mixerGroup.audioMixer.FindMatchingGroups("Music")[0];
                mAssist.music.Add(s);
            }
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
        sAssist.mixerGroup = mixerGroup.audioMixer.FindMatchingGroups("SFX")[0];

        if (sounds.Length > 0)
        {
            foreach (Sound s in sounds) //Init each sound - give it a source and init that source to make it playable
            {
                GameObject gObject = new GameObject();
                s.source = gObject.AddComponent<AudioSource>();
                gObject.name = s.clip.name;
                s.source.clip = s.clip;
                s.source.loop = s.loop;
                s.source.volume = s.volume;
                s.source.playOnAwake = false;

                gObject.transform.parent = sAssistGO.transform;

                s.source.outputAudioMixerGroup = mixerGroup.audioMixer.FindMatchingGroups("SFX")[0];
                sAssist.soundEffects.Add(s);
            }
        }
    }
    /// <summary>
    /// Checks whether the inputted sound is playing. If it is, passes 1. If not it passes 2. If it passes 0, the sound does not exist or is missing.
    /// </summary>
    /// <param name="sound"></param>
    /// <returns></returns>
    public int IsSoundPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound); //Find the sound we want to play, ensure it's not null
        if (s == null)
        {
            s = Array.Find(Music, item => item.name == sound); //If it's not a sound effect, see if it's a music track

            if (s == null)
            {
                Debug.LogError("Sound and/or Music: " + name + " not found!");
                return 0;
            }
        }

        //if the sound is playing, pass 1
        if(s.source.isPlaying)
        {
            return 1;
        }
        //if the sound is NOT playing, pass 2
        else
        {
            return 2;
        }
    }

    bool DoesSoundExist(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound); //Find the sound we want to play, ensure it's not null
        if (s == null)
        {
            s = Array.Find(Music, item => item.name == sound); //If it's not a sound effect, see if it's a music track

            if (s == null)
            {
                Debug.LogError("Sound and/or Music: " + name + " not found!");
                return false;
            }
        }

        return true;
    }

    public void StopAll()
    {
        foreach(Sound s in sounds)
        {
            s.source.Stop();
        }
    }
}
