using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketballManager : MonoBehaviour
{
    public int score = 0; // Score to keep track of goals
    public int scoreToWin = 3; // Number of goals to win
    public AudioClip goalSound; // Sound to play when goal is scored
    public AudioClip winSound;
    public AudioSource goalAudio;
    public AudioSource winAudio;
    public GameObject winCanvas; // Canvas object to activate when the player wins
    public GameObject infoCanvas;
    public GameObject[] lightbulbs; // Array of lightbulbs GameObjects
    private int currentIndex = 0; // Index to track the current lightbulb

    void Start()
    {
        // Ensure the canvas object is deactivated at the start
        winCanvas.SetActive(false);
        infoCanvas.SetActive(true);
    }

    // Function to be called whenever a goal is scored
    public void ScoredGoal()
    {

        score++; // Increment the score
        goalAudio.PlayOneShot(goalSound); // Play the goal sound

        // Get the current lightbulb GameObject
        GameObject currentLightbulb = lightbulbs[currentIndex];

        // Turn on emission for the material attached to the current lightbulb
        Renderer renderer = currentLightbulb.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.EnableKeyword("_EMISSION");
            //renderer.material.SetColor("_EmissionColor", Color.white); // Set emission color if needed
        }

        // Turn on the Light component on the current lightbulb
        Light lightComponent = currentLightbulb.GetComponent<Light>();
        if (lightComponent != null)
        {
            lightComponent.enabled = true;
        }

        // Increment the currentIndex for the next call
        currentIndex++;

        // If the score reaches the required number to win
        if (score >= scoreToWin)
        {
            // Activate the canvas object
            winCanvas.SetActive(true);

            // Deactivate the info canvas
            infoCanvas.SetActive(false);

            winAudio.Play();
        }
    }
}
