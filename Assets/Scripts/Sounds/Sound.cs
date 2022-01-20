using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Sound {

	public string name; //name of the sound, used to find it for scripts attempting to play

	public AudioClip clip; //Actual clip

	[Range(0f, 1f)] //Volume, sets it in a range with a handy scrollbar in the editor
	public float volume = 1f;

	[Range(1f, 3f)] //Same as volume but for pitch
	public float pitch = 1f;

	public bool loop = false; //Do I loop?
	 
	public AudioMixerGroup mixerGroup; //Is there a mixer?

	[HideInInspector]
	public AudioSource source; //Source associated with this sound - not in editor, controlled behind the scene

    float timeToFade;
    float dTime;

	public void SetValues(Sound input)
    {
		name = input.name;
		clip = input.clip;
		volume = input.volume;
		pitch = input.pitch;
		loop = input.loop;
		source = input.source;
    }
}
