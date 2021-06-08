using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SliderVolume : MonoBehaviour
{

    public AudioMixer audioMixer;
    public AudioMixer volumeMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetVolumeFX(float volume)
    {
        volumeMixer.SetFloat("volume", volume);
    }
}
