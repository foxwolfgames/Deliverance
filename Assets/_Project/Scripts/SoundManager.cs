using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource shootingSound;
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
}
