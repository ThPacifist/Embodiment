using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeVolume : MonoBehaviour
{
    [SerializeField]
    AudioMixerGroup mixerGroup;
    [SerializeField]
    string fieldName;

    public void SetVolume(float val)
    {
        mixerGroup.audioMixer.SetFloat(fieldName, Mathf.Log10(val) * 20);
    }
}
