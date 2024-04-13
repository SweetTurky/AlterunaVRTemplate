using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Alteruna;

public class MenuManagerS1 : MonoBehaviour
{

    public string sceneToLoad;
    public Multiplayer multiplayerManager;

    // Audio clips to play during the game intro
    public AudioClip[] introAudioClips;

    // Duration of the intro in seconds
    public float outroDuration = 30f;

    // Reference to the fade manager
    //public FadeManager fadeManager;

    // Start is called before the first frame update
    
    private void Start() {
        GameIntro();
    }
    public void GameIntro()
    {
        // Start playing the audio clips
        StartCoroutine(PlayIntroAudio());

        // Wait for the intro duration
        StartCoroutine(Outro());
    }

    IEnumerator PlayIntroAudio()
    {
        // Play each audio clip in the intro
        foreach (AudioClip clip in introAudioClips)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
            yield return new WaitForSeconds(clip.length);
        }
    }

    IEnumerator Outro()
    {
        // Wait for the intro duration
        yield return new WaitForSeconds(outroDuration);

        // Fade out all objects with the "Player" tag
        //FadeOutPlayers();

        //Search for the XR Interaction Manager, and make sure it is added to DontDestroyOnLoad
         GameObject obj = GameObject.Find("XRInteractionManager");
        if (obj != null)
        {
            Multiplayer.DontDestroyOnLoad(obj);
            Debug.Log("XR Interaction Manager added to Don'tDestroyOnLoad");
        }
        else
        {
            Debug.LogError("XR Interaction Manager not found!");
        }

        LoadNextScene();
    }

    /*void FadeOutPlayers()
    {
        // Find all objects with the "Player" tag
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Call the fade to black function on each player object
        foreach (GameObject player in players)
        {
            FadeToBlack fadeToBlack = player.GetComponent<FadeToBlack>();
            if (fadeToBlack != null)
            {
                fadeToBlack.StartFade(); // Assuming there's a function to start fading to black
            }
        }
    }*/

    void LoadNextScene()
    {
        // Load the next scene (you can specify the scene name or index here)
        multiplayerManager.LoadScene(sceneToLoad);
    }
}
