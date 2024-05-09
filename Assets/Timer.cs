using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public float timerDuration = 60f;
    private float currentTime;
    private bool timesUp = false;
    private bool interactedWith = false;

    // Update is called once per frame
    void Update()
    {
        if (!timesUp)
        {
            UpdateTimer();
            UpdateUIText();
        }
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            //Do stuff when timer reaches 0
            BasketballManager.Instance.CheckBothPlayersFinished();  
            timesUp = true;
        }
    }

    void UpdateUIText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{1:00}", minutes, seconds);
    }

    public void StartTimer()
    { 
        if(!interactedWith)
        if (BasketballManager.Instance.player1Finished && BasketballManager.Instance.player2Finished) 
        {
            currentTime = timerDuration;
            interactedWith = true;
        }
        else
        {
            return;
        }
    }

    public void Player1Finished()
    {
        BasketballManager.Instance.player1Finished = true;
    }
    public void Player2Finished()
    {
        BasketballManager.Instance.player2Finished = true;
    }
}
   
