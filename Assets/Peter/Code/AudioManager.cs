using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] musicClips;

    public void PlayMusic(int index)
    {
        if (index >= 0 && index < musicClips.Length)
        {
            musicSource.clip = musicClips[index];
            musicSource.Play();
        }
        else
        {
            Debug.LogError("Invalid music index!");
        }
    }

    // Add more audio management functions as needed
}
