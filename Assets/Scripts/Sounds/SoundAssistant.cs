using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundAssistant : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;
    public List<Sound> soundEffects = new List<Sound>();

    [HideInInspector]
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
