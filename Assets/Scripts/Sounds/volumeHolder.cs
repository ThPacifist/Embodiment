using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class volumeHolder : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetVolume(float val)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(val) * 20);
        Debug.Log("Volume is " + val);
    }
}
