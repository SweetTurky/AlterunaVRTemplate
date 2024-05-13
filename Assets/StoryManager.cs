using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get;}

    [Header("Wait Times")]
    public float waitTimeFromSceneStart = 10;

    [Header("Audio")]
    public AudioSource mainSong;

    [Header("Lists & Arrays")]
    public List<GameObject> artistsToActivate; 

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
        StartCoroutine(magnusVoice1.MagnusSpeakWithDelay(5f, magnusVoice1.magnusKoncertVoicelines, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ActivateArtists()
    {
        //Wait for a specified duration before firing methods below
        yield return new WaitForSeconds(waitTimeFromSceneStart);

        StartSong();

        foreach (var artist in artistsToActivate)
        {
            artist.SetActive(true);
        }

        HearingLossSimulation.Instance.hearinglossActivated = true;
    }

    public void StartSong()
    {
        mainSong.Play();
    }
}
