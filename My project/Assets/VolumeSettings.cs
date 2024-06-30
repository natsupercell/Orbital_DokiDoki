using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    public void SetMusicVolume()
    {
        float music = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(music)*20);
    }
    public void SetSFXVolume()
    {
        float SFX = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(SFX) * 20);
    }
}