using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

	public string name; //name of the sound, used to find it for scripts attempting to play

	public AudioClip clip; //Actual clip

	[Range(0f, 1f)] //Volume, sets it in a range with a handy scrollbar in the editor
	public float volume = 1f;

	[Range(1f, 3f)] //Same as volume but for pitch
	public float pitch = 1f;

	public bool loop = false; //Do I loop?
	 
	public AudioMixerGroup mixerGroup; //Is there a mixer?

	//[HideInInspector]
	public AudioSource source; //Source associated with this sound - not in editor, controlled behind the scene

    float timeToFade;
    float dTime;

    #region get/set
    public void setFadeTime(float nF)
    {
        timeToFade = nF;
    }
    #endregion

    public bool AudioFadeOut()
    { 
        float startVolume = this.source.volume;

        Debug.Log("We are in the fade function: " + this.name);

        if (this.source.isPlaying)
        {
            if (this.source.volume <= 0)
            {
                this.source.Stop();
                this.source.volume = startVolume;
                dTime = 0f;
                return true;
            }
            else
            {
                dTime += Time.deltaTime;
                if(dTime > timeToFade)
                {
                    dTime = timeToFade;
                }
                this.source.volume -= startVolume * (dTime / timeToFade);
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
