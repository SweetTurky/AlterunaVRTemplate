using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10f; // Set the countdown time
    public bool timerIsRunning = false;
    public TMP_Text timeText;
    private BasketballManager basketballManager;

    private void Start()
    {
        // Start the timer automatically
        timerIsRunning = true;
        basketballManager = FindObjectOfType<BasketballManager>();
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimerEnd();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        // Check if the game has finished
        if (basketballManager.playerFinished)
        {
            Debug.Log("Player has finished the game.");
        }
        else
        {
            Debug.Log("Player did not finish in time.");
            // You can add additional logic here if needed
        }
    }
}
