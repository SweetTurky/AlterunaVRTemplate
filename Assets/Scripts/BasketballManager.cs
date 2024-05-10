using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Alteruna;

public class BasketballManager : MonoBehaviour
{
    private static BasketballManager _instance;

    public static BasketballManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BasketballManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(BasketballManager).Name);
                    _instance = singletonObject.AddComponent<BasketballManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    [Header("Player1")]
    public int scorePlayer1 = 0; // Score to keep track of player 1's goals
    public TMP_Text scoreTextP1;
    public GameObject winCanvasPlayer1; // Canvas object for player 1
    public GameObject infoCanvasPlayer1;
    public GameObject[] lightbulbsPlayer1; // Array of lightbulbs GameObjects for player 1
    public AudioSource goalAudioPlayer1; // Audio source for player 1
    public bool player1Finished = false; // Flag to indicate if player 1 has finished
    public AudioSource winAudioPlayer1;
    public GameObject countdownP1;
    public Text countdownTextP1; // Reference to the Text component on the canvas


    [Header("Player2")]
    public int scorePlayer2 = 0; // Score to keep track of player 2's goals
    public TMP_Text scoreTextP2;
    public GameObject winCanvasPlayer2; // Canvas object for player 2
    public GameObject infoCanvasPlayer2;
    public GameObject[] lightbulbsPlayer2; // Array of lightbulbs GameObjects for player 2
    public AudioSource goalAudioPlayer2; // Audio source for player 2
    public bool player2Finished = false; // Flag to indicate if player 2 has finished
    public AudioSource winAudioPlayer2;
    public GameObject countdownP2;
    public Text countdownTextP2;


    [Header("Shared")]
    public int scoreToWin = 3; // Number of goals to win for each player
    public AudioClip goalSound; // Sound to play when goal is scored
    public AudioClip winSound;
    private float countdownTimer = 10f; // Timer for the countdown



    // Start is called before the first frame update
    void Start()
    {
        // Ensure the canvas objects are deactivated at the start
        winCanvasPlayer1.SetActive(false);
        winCanvasPlayer2.SetActive(false);
        infoCanvasPlayer1.SetActive(true);
        infoCanvasPlayer2.SetActive(true);
    }

    private void Update()
    {
        if (countdownP1.active && countdownP2.active)
        {
            countdownTextP1.text = "Godt gået! Oplevelsen starter om: " + Mathf.Round(countdownTimer).ToString();
            countdownTextP2.text = "Godt gået! Oplevelsen starter om: " + Mathf.Round(countdownTimer).ToString();
        }
        scoreTextP1.text = scorePlayer1.ToString();
        scoreTextP2.text = scorePlayer2.ToString();
    }

    // Function to be called whenever a goal is scored
    public void ScoredGoal(string playerTag)
    {
        if (playerTag == "Player1")
        {
            scorePlayer1++; // Increment player 1's score
            goalAudioPlayer1.PlayOneShot(goalSound); // Play the goal sound for player 1

            // Turn on emission and light for the current lightbulb for player 1
            if (scorePlayer1 <= scoreToWin && scorePlayer1 <= lightbulbsPlayer1.Length)
            {
                GameObject currentLightbulb = lightbulbsPlayer1[scorePlayer1 - 1];
                Renderer renderer = currentLightbulb.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.EnableKeyword("_EMISSION");
                }
                Light lightComponent = currentLightbulb.GetComponent<Light>();
                if (lightComponent != null)
                {
                    lightComponent.enabled = true;
                }
            }

            // If player 1 reached the score to win
            /*if (scorePlayer1 >= scoreToWin && !player1Finished)
            {
                infoCanvasPlayer1.SetActive(false);
                winCanvasPlayer1.SetActive(true);
                winAudioPlayer1.PlayOneShot(winSound);
                player1Finished = true;
                CheckBothPlayersFinished();
            }*/
        }
        else if (playerTag == "Player2")
        {
            scorePlayer2++; // Increment player 2's score
            goalAudioPlayer2.PlayOneShot(goalSound); // Play the goal sound for player 2

            // Turn on emission and light for the current lightbulb for player 2
            if (scorePlayer2 <= scoreToWin && scorePlayer2 <= lightbulbsPlayer2.Length)
            {
                GameObject currentLightbulb = lightbulbsPlayer2[scorePlayer2 - 1];
                Renderer renderer = currentLightbulb.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.EnableKeyword("_EMISSION");
                }
                Light lightComponent = currentLightbulb.GetComponent<Light>();
                if (lightComponent != null)
                {
                    lightComponent.enabled = true;
                }
            }

            // If player 2 reached the score to win
            /*if (scorePlayer2 >= scoreToWin && !player2Finished)
            {
                infoCanvasPlayer2.SetActive(false);
                winCanvasPlayer2.SetActive(true);
                winAudioPlayer2.PlayOneShot(winSound);
                player2Finished = true;
                CheckBothPlayersFinished();
            }*/
        }
    }

    // Method to check if both players have finished
    public void CheckBothPlayersFinished()
    {
        if (player1Finished && player2Finished)
        {
            // Both players have finished, execute methods
            StartCoroutine("BothPlayersFinishedCoroutine");
        }
    }

    public IEnumerator BothPlayersFinishedCoroutine()
    {
        ////winCanvasPlayer1.SetActive(false);
        countdownP1.SetActive(true);
        countdownP2.SetActive(true);
        //multiplayerManager = GameObject.FindWithTag("MultiplayerManagerTag");
        //multiplayerManager.GetComponent<MultiplayerManager>();
        // Coroutine logic here
        Debug.Log("Both players finished!");
        //textToDisable.enabled = false;

        // Countdown loop
        while (countdownTimer > 0)
        {
            yield return null; // Wait for the next frame
            countdownTimer -= Time.deltaTime; // Update the timer based on frame time
        }

        Multiplayer.Instance.LoadScene("Koncert");
    }
}
