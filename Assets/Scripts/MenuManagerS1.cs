using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Alteruna;

public class MenuManagerS1 : MonoBehaviour
{
    public string sceneToLoad;
    //public Multiplayer instance;
    public Text countdownText; // Reference to the Text component on the canvas
    public Text textToDisable;
    private float countdownTimer = 10f; // Timer for the countdown
    //private bool allPlayersConnected = false;
    private int playerCount = 0;

    public bool debugMode = false;

    // Start is called before the first frame update
    private void Start() 
    {
        countdownText.enabled = false;
    }
    private void Update()
    {
        // Check if both Player1 and Player2 are connected
        /*if (!allPlayersConnected && IsPlayerConnected("Player1") && IsPlayerConnected("Player2"))
        {
            allPlayersConnected = true;
            countdownText.enabled = true;
            StartCoroutine(AllPlayersConnectedCoroutine());
            // Update the countdown text
            
        }*/
        if (countdownText.enabled == true) 
        {
            countdownText.text = "Game starting in: " + Mathf.Round(countdownTimer).ToString();
        }
        
    }

    public void IsPlayerConnected()
    {
        Debug.Log("1 player connected");
        playerCount++;
        if (!debugMode && playerCount == 2)
        {
            Debug.Log("2 players connected, starting game...");
            StartCoroutine("AllPlayersConnectedCoroutine");
            countdownText.enabled = true;
        }
        else if (debugMode && playerCount == 1)
        {
            Debug.Log("1 players connected, starting game in debug mode...");
            StartCoroutine("AllPlayersConnectedCoroutine");
            countdownText.enabled = true;
        }
    }

    private IEnumerator AllPlayersConnectedCoroutine()
    {
        // Coroutine logic here
        Debug.Log("Both players connected!");
        textToDisable.enabled = false;

        // Countdown loop
        while (countdownTimer > 0)
        {
            yield return null; // Wait for the next frame
            countdownTimer -= Time.deltaTime; // Update the timer based on frame time
        }

        // When the countdown is finished, load the next scene
        LoadNextScene();
    }

    void LoadNextScene()
    {
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
        // Load the next scene (you can specify the scene name or index here)
        Multiplayer.Instance.LoadScene(sceneToLoad);
    }

    public void SetTag()
    {
        int userNumber = Multiplayer.Instance.GetUser().Index;
        Debug.Log("" + userNumber);
    }
}
