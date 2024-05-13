using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnusVoice : MonoBehaviour
{
    public AudioClip[] magnusKoncertVoicelines;
    public AudioClip[] magnusKlatreVoicelines;
    public AudioClip[] magnusEarpongVoicelines;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MagnusSpeak(AudioClip[] voicelines, int index)
    {
        if (voicelines != null && index >= 0 && index < voicelines.Length)
        {
            AudioSource.PlayClipAtPoint(voicelines[index], transform.position);
        }
        else
        {
            Debug.LogWarning("Invalid voiceline index or voicelines array is null or empty.");
        }
    }

    public IEnumerator MagnusSpeakWithDelay(float delay, AudioClip[] voicelines, int index)
    {
        yield return new WaitForSeconds(delay);
        MagnusSpeak(voicelines, index);
    }

    // Example usage:
    // Call MagnusSpeak(magnusKoncertVoicelines, index) to play a specific voiceline immediately from the given array of voicelines.
    // Call StartCoroutine(MagnusSpeakWithDelay(delayTime, magnusKoncertVoicelines, index)) to play a specific voiceline after a delay.
}
