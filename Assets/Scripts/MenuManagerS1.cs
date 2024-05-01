using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Alteruna;

public class MenuManagerS1 : MonoBehaviour
{
    public string sceneToLoad;
    public Multiplayer multiplayerManager;

    public Text countdownText; // Reference to the Text component on the canvas
    public Text textToDisable;
    private float countdownTimer = 10f; // Timer for the countdown
    private bool allPlayersConnected = false;

    // Start is called before the first frame update

    private void Update()
    {
        // Check if both Player1 and Player2 are connected
        if (!allPlayersConnected && IsPlayerConnected("Player1") && IsPlayerConnected("Player2"))
        {
            allPlayersConnected = true;
            StartCoroutine(AllPlayersConnectedCoroutine());
        }

        // Update the countdown text
        countdownText.text = "Game starting in: " + Mathf.Round(countdownTimer).ToString();
    }

    private bool IsPlayerConnected(string tag)
    {
        // Find all game objects with the given tag
        GameObject[] players = GameObject.FindGameObjectsWithTag(tag);

        // Return true if at least one object is found with the given tag
        return players.Length > 0;
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
        multiplayerManager.LoadScene(sceneToLoad);
    }
}
