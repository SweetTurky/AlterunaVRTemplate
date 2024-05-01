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

        // If the score reaches the required number to win
        if (score >= scoreToWin)
        {
            // Activate the canvas object
            winCanvas.SetActive(true);

            //Deactivate the info canvas
            infoCanvas.SetActive(false);   

            winAudio.Play();           
        }
    }
}
