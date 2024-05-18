using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Alteruna;
public class StoryManager : AttributesSync
{
    public static StoryManager Instance { get; set; }

    [Header("Wait Times")]
    public float waitTimeFromSceneStart = 10f;

    [Header("Audio Sources")]
    public AudioSource mainSong;
    public AudioSource announcerBefore;
    public AudioSource announcerAfter;
    public AudioSource crowdAudio1;
    public AudioSource crowdAudio2;
    public AudioSource crowdAudio3;
    public AudioSource crowdMumble;
    public AudioSource crowdMumble1;
    

    [Header("Lists & Arrays")]
    public List<GameObject> artistsToActivate;
    public List<GameObject> objectsToActivate;
    public List<GameObject> objectsToDeactivate;

    [Header("P1 and P2")]
    public bool p1Ready;
    public bool p2Ready;

    [Header("Magnus")]
    public GameObject magnus1;
    public GameObject magnus2;
    

    private MagnusVoice magnusVoice1;
    private MagnusVoice magnusVoice2;
    

    [Header("Script References")]
    private MessageAllPlayers messageAllPlayers;

    private void Awake()
    {
        // Get MagnusVoice components from GameObjects and store references
        magnusVoice2 = magnus2.GetComponent<MagnusVoice>();
        magnusVoice1 = magnus1.GetComponent<MagnusVoice>();
        messageAllPlayers = GetComponent<MessageAllPlayers>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(nameof(PlayBeerPongVoiceLines)); //Out comment
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

    public void PlayersReady()
    {
        BroadcastRemoteMethod(nameof(ClimbReadyVO));
    }

    public void PlayersReady2()
    {
        if (p1Ready && p2Ready)
        {
            BroadcastRemoteMethod(nameof(LoadNextScene));
        }
    }

    [SynchronizableMethod]
    private void LoadNextScene()
    {
        GameObject obj = GameObject.Find("HearingLossSimulator");
        if (obj != null)
        {
            Multiplayer.DontDestroyOnLoad(obj);
            Debug.Log("XR Interaction Manager added to Don'tDestroyOnLoad");
        }
        else
        {
            Debug.LogError("HearingLossSimulator not found!");
        }
        Multiplayer.LoadScene(3);
    }

    [SynchronizableMethod]
    public void ClimbReadyVO()
    {
        if (p1Ready && p2Ready)
        {
            magnusVoice2.MagnusSpeak(magnusVoice2.magnusKlatreVoicelines, 0);
        }
    }

    [SynchronizableMethod]
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
    
}
