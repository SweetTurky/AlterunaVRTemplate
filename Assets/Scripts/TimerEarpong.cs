using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerEarpong : MonoBehaviour
{
    public bool player1Ready = false;
    public bool player2Ready = false;

    public TextMeshProUGUI timerText; // Reference to the TextMeshPro UI element

    private bool timerStarted = false;
    private float timerDuration = 5 * 60; // 5 minutes in seconds
    private float elapsedTime = 0;

    void Update()
    {
        // Check if the timer has started and is still running
        if (timerStarted && elapsedTime < timerDuration)
        {
            elapsedTime += Time.deltaTime; // Update the elapsed time

            // Update the timer display
            UpdateTimerDisplay();

            // Check if both players are ready and the timer is up
            if (player1Ready && player2Ready && elapsedTime >= timerDuration)
            {
                StoryManager.Instance.EndGame(); // Call the EndGame method from EndGameScript
            }
        }
    }

    // Method to start the timer
    public void StartTimer()
    {
        if (!timerStarted && player1Ready && player2Ready)
        {
            timerStarted = true;
        }
    }

    // Update the timer display
    void UpdateTimerDisplay()
    {
        float timeRemaining = timerDuration - elapsedTime;
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;
    }
    public void P1Ready()
    {
        player1Ready = true;
    }
    public void P2Ready()
    {
        player2Ready = true;
    }
}

