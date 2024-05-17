using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Alteruna;

public class Timer : AttributesSync
{
    public TMP_Text timerText;
    public float timerDuration = 60f;
    private float currentTime;
    private bool timerStarted = false;
    private BasketballManager basketballManager;

    private void Start()
    {
        // Find the GameObject with the name "BasketballManager" and get its BasketBallManager component
        basketballManager = GameObject.Find("BallGameManager").GetComponent<BasketballManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Debug.Log("Time's up!");
                currentTime = 0;
                //Do stuff when timer reaches 0
                BroadcastRemoteMethod(nameof(TutorialDone));
            }
            UpdateUIText();
        }
    }

    void UpdateUIText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{1:00}", minutes, seconds);
    }

    public void SendTimerStartRPC()
    {
        BroadcastRemoteMethod("StartTimer");
    }

    [SynchronizableMethod]
    public void StartTimer()
    {
        timerStarted = true;
        currentTime = timerDuration;
    }

    [SynchronizableMethod]
    private void TutorialDone()
    {
        Multiplayer.Instance.LoadScene("Koncert");
    }

    public void Player1Finished()
    {
        basketballManager.player1Finished = true;
        Debug.Log("Player 1 Finished");
    }
    public void Player2Finished()
    {
        basketballManager.player2Finished = true;
        Debug.Log("Player 2 Finished");
    }
}

