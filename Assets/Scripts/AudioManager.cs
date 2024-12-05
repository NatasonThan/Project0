using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip bg;
    public AudioClip gameOver;
    public AudioClip bubble;
    public AudioClip eat;
    public AudioClip hit;
    public AudioClip pickItem;

    private void Start()
    {
        musicSource.clip = bg;
    }
    public void PlaySFX(AudioClip clip) 
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PauseBG() 
    {
        musicSource.Pause();
    }
    public void PlayBG() 
    {
        musicSource.clip = bg;
        musicSource.Play();
    }
}
