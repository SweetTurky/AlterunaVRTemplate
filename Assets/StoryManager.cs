using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Alteruna;
using UnityEngine.SceneManagement;

public class StoryManager : AttributesSync
{
    public static StoryManager Instance { get; set; }

    public bool readyForBeerpong = false;

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
        // Start various coroutines for the concert scene
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

        // Automatically trigger the climb voice over after the concert ends
        StartCoroutine(TriggerClimbReadyVOAfterDelay(250f)); // Adjust the delay as needed
    }

    void Update()
    {
        if (readyForBeerpong == true)
        {
            SceneManager.LoadScene(2); // Load the next scene specified in sceneToLoad
        }
    }

    public IEnumerator ActivateArtists()
    {
        // Wait for a specified duration before firing methods below
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

    private IEnumerator TriggerClimbReadyVOAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClimbReadyVO();
    }


    public void LastScene()
    {
        readyForBeerpong = true;
    }
    [SynchronizableMethod]
    public void LoadNextScene()
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
        Debug.LogError("Loader næste scene");
        Multiplayer.LoadScene(3); // er ændret til index 2 fra 3. Ændr tilbage, hvis det ikke virker.
    }

    [SynchronizableMethod]
    public void ClimbReadyVO()
    {
        magnusVoice2.MagnusSpeak(magnusVoice2.magnusKlatreVoicelines, 0);
    }

    [SynchronizableMethod]
    public void Activate()
    {
        foreach (var gameObject in objectsToActivate)
        {
            gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {
        foreach (var gameObject in objectsToDeactivate)
        {
            gameObject.SetActive(false);
        }
    }
}
