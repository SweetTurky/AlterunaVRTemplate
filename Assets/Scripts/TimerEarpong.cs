using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Alteruna;

public class TimerEarpong : AttributesSync
{
    private LastSceneManager lastSceneManager;
    public bool player1Ready = false;
    public bool player2Ready = false;

    public TextMeshProUGUI timerText; // Reference to the TextMeshPro UI element

    private bool timerStarted = false;
    private float timerDuration = 6 * 60; // 6 minutes in seconds
    private float elapsedTime = 0;

    private void Start() 
    {
        lastSceneManager = GetComponent<LastSceneManager>();
        //storyManager.p1Ready = true;
        //storyManager.p2Ready = true;
        //TimerStartRPC();
        Invoke("StartTimer", 2f);
    }

    void Update()
    {
        // Check if the timer has started and is still running
        if (timerStarted && elapsedTime < timerDuration)
        {
            elapsedTime += Time.deltaTime; // Update the elapsed time

            // Update the timer display
            UpdateTimerDisplay();

            // Check if both players are ready and the timer is up
            if (lastSceneManager.p1Ready && lastSceneManager.p2Ready && elapsedTime >= timerDuration)
            {
                //storyManager.EndGameTimer(); // Call the EndGame method the StoryManager
                elapsedTime = 0;
            }
        }
    }

    public void StartTimer()
    {
        if (!timerStarted && lastSceneManager.p1Ready && lastSceneManager.p2Ready)
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
}

