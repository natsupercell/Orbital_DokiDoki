using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------------- Audio Source -----------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------------- Audio Source -----------------")]
    public AudioClip background;
    public AudioClip spawn;
    public AudioClip shooting;
    public AudioClip laser;
    public AudioClip shootRocket;
    public AudioClip rocketExplode;
    public AudioClip flame;
    public AudioClip orbCollect;
    public AudioClip click;

    private void Start()
    {
       musicSource.clip = background;
       //musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
