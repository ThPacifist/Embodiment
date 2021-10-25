using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeHolder : MonoBehaviour
{
    public static volumeHolder instance;
    [SerializeField]
    float volume = 1;
    AudioManager manager;

    // Start is called before the first frame update
    void Start()
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

        setVolume();

    }

    public void OnSliderValueChanged(float value)
    {
        volume = value;
        setVolume();
    }

    public void setVolume()
    {
        manager = FindObjectOfType<AudioManager>();
        manager.updateVolume(volume);
    }
}
