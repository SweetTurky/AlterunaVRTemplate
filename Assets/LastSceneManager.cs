using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class LastSceneManager : AttributesSync
{
    public GameObject magnus3;
    public MagnusVoice magnusVoice3;
    public MagnusAISoff magnusAiSoff;
    public AudioSource magnusAudioSource;
    public AudioSource outroSpeak;

    public bool p1Ready;
    public bool p2Ready;
    // Start is called before the first frame update
    void Start()
    {
        magnusVoice3 = magnus3.GetComponent<MagnusVoice>();
    }

    public void ReadyToPlayVO()
    {
        if (p1Ready && p2Ready)
        {
            Debug.Log("Starting Speak...");
            magnusVoice3.MagnusSpeak(magnusVoice3.magnusEarpongVoicelines, 0);
            //magnusVoice3.MagnusSpeakWithDelay(14f, magnusVoice3.magnusEarpongVoicelines, 1);

            // Convert array to a list
            List<AudioClip> voicelinesList = new List<AudioClip>(magnusVoice3.magnusEarpongVoicelines);

            // Remove the first two elements
            voicelinesList.RemoveRange(0, 1);

            // Convert list back to array
            magnusVoice3.magnusEarpongVoicelines = voicelinesList.ToArray();

            StartCoroutine(nameof(PlayBeerPongVoiceLines));
        }
    }

    private IEnumerator PlayBeerPongVoiceLines()
    {
        Debug.Log("Beerpongvoicelines activated");
        yield return new WaitForSeconds(11f);
        //int totalVoicelines = magnusVoice3.magnusEarpongVoicelines.Length;

        foreach (AudioClip clip in magnusVoice3.magnusEarpongVoicelines)
        {
            // Set the clip to be played by the AudioSource
            magnusAudioSource.clip = clip;
            // Play the clip through the AudioSource
            magnusAudioSource.Play();

            // Play talking animation and remain stationary while the voice line is playing
            if (magnusAiSoff._animator != null)
            {
                magnusAiSoff._animator.SetBool("IsWalking", false);
                magnusAiSoff._animator.SetBool("IsTalking", true);
            }

            // Wait for the audio clip to start playing
            //yield return WaitForAudioClip(magnusAudioSource);

            // Wait for the duration of the clip
            yield return new WaitForSeconds(clip.length);

            // Resume walking animation after the voice line is finished playing
            if (magnusAiSoff._animator != null)
            {
                magnusAiSoff._animator.SetBool("IsTalking", false);
            }

            // Call SetNextWaypoint to make the agent walk towards the next waypoint
            magnusAiSoff.SetNextWaypoint();
            // Resume walking during the 15-second pause
            float elapsedTime = 0f;
            while (elapsedTime < 15f)
            {
                // Update elapsed time
                elapsedTime += Time.deltaTime;

                if (magnusAiSoff._agent.remainingDistance > magnusAiSoff._agent.stoppingDistance)
                {
                    magnusAiSoff._animator.SetBool("IsWalking", true);
                    magnusAiSoff._animator.SetBool("IsTalking", false);
                }
                else if (magnusAiSoff._agent.remainingDistance < magnusAiSoff._agent.stoppingDistance)
                {
                    magnusAiSoff._animator.SetBool("IsWalking", false);
                }
                yield return null; // Yielding here ensures the loop will continue in the next frame
            }
            magnusAiSoff.transform.LookAt(magnusAiSoff.lookAtObject.transform.position);
        }
        EndGameTimer();
    }

    public void EndGameTimer()
    {
        BroadcastRemoteMethod(nameof(EndGame));
    }

    [SynchronizableMethod]
    private void EndGame()
    {
        Debug.Log("Spillet slutter nu");
        outroSpeak.Play();
        StartCoroutine("QuitGame");
    }

    
    public IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(10f);
        Application.Quit();
    }

}
