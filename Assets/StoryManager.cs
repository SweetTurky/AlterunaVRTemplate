using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [Header("Lists & Arrays")]
    public List<GameObject> artistsToActivate;

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
        StartCoroutine(magnusVoice1.MagnusSpeakWithDelay(237f, magnusVoice1.magnusKoncertVoicelines, 4));
        StartCoroutine(PlayAudioSource(26f, announcerBefore));
        StartCoroutine(PlayAudioSource(28f, crowdAudio1));
        StartCoroutine(PlayAudioSource(30f, crowdAudio2));
        StartCoroutine(PlayAudioSource(32f, crowdAudio3));
        StartCoroutine(PlayAudioSource(203f, announcerAfter));
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

    public void P1ReadyToClimb()
    {
        p1Ready = true;
    }

    public void P2ReadyToClimb()
    {
        p2Ready = true;
    }

    public void ClimbReadyVO()
    {
        if (p1Ready && p2Ready)
        {
            magnusVoice2.MagnusSpeak(magnusVoice2.magnusKlatreVoicelines, 0);
        }       
    }

}
