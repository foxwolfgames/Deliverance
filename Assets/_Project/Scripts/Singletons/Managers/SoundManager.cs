using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource shootingChannel;
    public AudioClip shotSoundClip;
    public AudioSource reloadingSound;
    public AudioSource emptyMagazineSound;
    
    private void Awake()
    {
        if (Instance != null && Instance!= this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingSound()
    {
        shootingChannel.PlayOneShot(shotSoundClip);
    }
}
