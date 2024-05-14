using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; set; }

    [Header("Wait Times")]
    public float waitTimeFromSceneStart = 10f;

    [Header("Audio Sources")]
    public AudioSource mainSong;
    public AudioSource announcerBefore;
    public AudioSource announcerAfter;
    public AudioSource outroSpeak;
    public AudioSource crowdAudio1;
    public AudioSource crowdAudio2;
    public AudioSource crowdAudio3;
    public AudioSource crowdMumble;
    public AudioSource crowdMumble1;
    public AudioSource magnusAudioSource;

    [Header("Lists & Arrays")]
    public List<GameObject> artistsToActivate;
    public List<GameObject> objectsToActivate;
    public List<GameObject> objectsToDeactivate;

    [Header("P1 and P2")]
    private bool p1Ready;
    private bool p2Ready;

    [Header("Magnus")]
    public GameObject magnus1;
    public GameObject magnus2;
    public GameObject magnus3;

    private MagnusVoice magnusVoice1;
    private MagnusVoice magnusVoice2;
    private MagnusVoice magnusVoice3;

    [Header("Script References")]
    public MagnusAISoff magnusAiSoff;

    private void Awake()
    {
        // Get MagnusVoice components from GameObjects and store references
        magnusVoice1 = magnus1.GetComponent<MagnusVoice>();
        magnusVoice2 = magnus2.GetComponent<MagnusVoice>();
        magnusVoice3 = magnus3.GetComponent<MagnusVoice>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(ActivateArtists));
        StartCoroutine(magnusVoice1.MagnusSpeakWithDelay(4f, magnusVoice1.magnusKoncertVoicelines, 0));
        StartCoroutine(magnusVoice1.MagnusSpeakWithDelay(8.5f, magnusVoice1.magnusKoncertVoicelines, 1));
        StartCoroutine(magnusVoice1.MagnusSpeakWithDelay(60f, magnusVoice1.magnusKoncertVoicelines, 2));
        StartCoroutine(magnusVoice1.MagnusSpeakWithDelay(212f, magnusVoice1.magnusKoncertVoicelines, 3));
        StartCoroutine(magnusVoice1.MagnusSpeakWithDelay(239f, magnusVoice1.magnusKoncertVoicelines, 4));
        Invoke(nameof(Deactivate), 215f);
        StartCoroutine(PlayAudioSource(230f, crowdMumble));
        StartCoroutine(PlayAudioSource(230f, crowdMumble1));
        Invoke(nameof(Activate), 248f);
        StartCoroutine(PlayAudioSource(26f, announcerBefore));
        StartCoroutine(PlayAudioSource(28f, crowdAudio1));
        StartCoroutine(PlayAudioSource(30f, crowdAudio2));
        StartCoroutine(PlayAudioSource(32f, crowdAudio3));
        StartCoroutine(PlayAudioSource(203f, announcerAfter));

        //ReadyToPlayVO(); // SKAL FJERNES IGEN!
    }

    public IEnumerator ActivateArtists()
    {
        //Wait for a specified duration before firing methods below
        yield return new WaitForSeconds(waitTimeFromSceneStart);

        StartConcertSong();

        foreach (var artist in artistsToActivate)
        {
            artist.SetActive(true);
        }

        HearingLossSimulation.Instance.hearinglossActivated = true;
    }

    public void StartConcertSong()
    {
        mainSong.Play();
    }

    public IEnumerator PlayAudioSource(float delay, AudioSource audioSource)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }

    public void P1Ready()
    {
        p1Ready = true;
    }

    public void P2Ready()
    {
        p2Ready = true;
    }

    public void ClimbReadyVO()
    {
        if (p1Ready && p2Ready)
        {
            magnusVoice2.MagnusSpeak(magnusVoice2.magnusKlatreVoicelines, 0);
            p1Ready = false;
            p2Ready = false;
        }
    }

    public void ReadyToPlayVO()
    {
        p1Ready = true; // Slet mig
        p2Ready = true; // Slet mig
        if (p1Ready && p2Ready)
        {
            magnusVoice3.MagnusSpeak(magnusVoice3.magnusEarpongVoicelines, 0);
            //magnusVoice3.MagnusSpeakWithDelay(14f, magnusVoice3.magnusEarpongVoicelines, 1);

            // Convert array to a list
            List<AudioClip> voicelinesList = new List<AudioClip>(magnusVoice3.magnusEarpongVoicelines);

            // Remove the first two elements
            voicelinesList.RemoveRange(0, 1);

            // Convert list back to array
            magnusVoice3.magnusEarpongVoicelines = voicelinesList.ToArray();

            StartCoroutine("PlayBeerPongVoiceLines");

        }
    }
    public void Activate()
    {
        foreach (var GameObject in objectsToActivate)
        {
            GameObject.SetActive(true);
        }
    }
    public void Deactivate()
    {
        foreach (var GameObject in objectsToDeactivate)
        {
            GameObject.SetActive(false);
        }
    }


    private IEnumerator PlayBeerPongVoiceLines()
    {

        Debug.Log("Beerpongvoicelines aktiveret");
        yield return new WaitForSeconds(11f);
        int totalVoicelines = magnusVoice3.magnusEarpongVoicelines.Length;
        foreach (AudioClip clip in magnusVoice3.magnusEarpongVoicelines)
        {
            AudioSource.PlayClipAtPoint(clip, magnus3.transform.position);
            yield return new WaitForSeconds(clip.length + 15f); // 20f is delay between voicelines
            magnusAiSoff._isWalking = true;
        }
    }

    public void EndGame()
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
