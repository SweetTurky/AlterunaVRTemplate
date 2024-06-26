using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BasketballManager : MonoBehaviour
{
    [Header("Player")]
    public int score = 0; // Score to keep track of the player's goals
    public TMP_Text scoreText;
    public GameObject winCanvas; // Canvas object for the player
    public GameObject infoCanvas;
    public GameObject[] lightbulbs; // Array of lightbulbs GameObjects for the player
    public AudioSource goalAudio; // Audio source for the player
    public bool playerFinished = false; // Flag to indicate if the player has finished
    public AudioSource winAudio;
    public GameObject countdown;
    public Text countdownText; // Reference to the Text component on the canvas

    [Header("Shared")]
    public int scoreToWin = 5; // Number of goals to win for the player
    public AudioClip goalSound; // Sound to play when goal is scored
    public AudioClip winSound;
    private float countdownTimer = 10f; // Timer for the countdown
    public string sceneToLoad; // Name of the scene to load after winning

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the canvas objects are deactivated at the start
        winCanvas.SetActive(false);
        infoCanvas.SetActive(true);
    }

    private void Update()
    {
        if (countdown.active)
        {
            countdownText.text = "Godt gået! Oplevelsen starter om: " + Mathf.Round(countdownTimer).ToString();
        }
        scoreText.text = score.ToString();
    }

    // Function to be called whenever a goal is scored
    public void ScoredGoal()
    {
        score++; // Increment the player's score
        goalAudio.PlayOneShot(goalSound); // Play the goal sound for the player

        // Turn on emission and light for the current lightbulb
        if (score <= scoreToWin && score <= lightbulbs.Length)
        {
            GameObject currentLightbulb = lightbulbs[score - 1];
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

        // Check if the player has won
        if (score >= scoreToWin)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        playerFinished = true;
        winCanvas.SetActive(true);
        infoCanvas.SetActive(false);
        winAudio.PlayOneShot(winSound);

        // Load the next scene after a short delay (adjust as needed)
        Invoke("LoadNextScene", 3.0f); // Example: Load after 3 seconds
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad); // Load the next scene specified in sceneToLoad
    }
}
