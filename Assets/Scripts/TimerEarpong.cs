using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Alteruna;

public class TimerEarpong : AttributesSync
{
    private LastSceneManager lastSceneManager;
    public bool player1Ready = false;

    public TextMeshProUGUI timerText; // Reference to the TextMeshPro UI element

    private bool timerStarted = false;
    private float timerDuration = 6 * 60; // 6 minutes in seconds
    private float elapsedTime = 0;

    private void Start() 
    {
        lastSceneManager = GetComponent<LastSceneManager>();
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

            // Check if player1 is ready and the timer is up
            if (lastSceneManager.p1Ready && elapsedTime >= timerDuration)
            {
                elapsedTime = 0;
            }
        }
    }

    public void StartTimer()
    {
        if (!timerStarted && lastSceneManager.p1Ready)
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
