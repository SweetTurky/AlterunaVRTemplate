using System.Collections;
using UnityEngine;

public class BasketballBehaviour : MonoBehaviour
{
    public Transform spawnPoint; // Spawn point for the ball
    public BasketballManager basketballManager; // Reference to the BasketballManager script
    public float goalCooldown = 2f; // Cooldown duration between goals
    public bool isScored = false;
    private Rigidbody rb;
    private bool canScore = true; // Flag to control scoring cooldown

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Function to reset the ball to the spawn point
    void ResetBall()
    {
        // Reset the ball to its assigned spawn point
        transform.position = spawnPoint.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isScored = false;
        canScore = true; // Reset the scoring cooldown flag
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if scoring is allowed (not in cooldown) and if the collided object has the BasketballManager script attached
        if (canScore && other.gameObject.GetComponent<BasketballManager>() != null)
        {
            // Determine which player scored based on the ball's tag
            string playerTag = DeterminePlayerTag(gameObject);

            // Call ScoredGoal method of BasketballManager with the player's tag
            basketballManager.ScoredGoal(playerTag);
            isScored = true;
            Debug.Log("You scored!");

            // Start the scoring cooldown
            StartCoroutine(StartGoalCooldown());

            // Reset the ball to its spawn point
            StartCoroutine(RespawnAfterDelay(2f));
        }
    }

    // Method to determine which player scored based on the ball's tag
    private string DeterminePlayerTag(GameObject basketball)
    {
        if (basketball.CompareTag("Player1Basketball"))
        {
            return "Player1";
        }
        else if (basketball.CompareTag("Player2Basketball"))
        {
            return "Player2";
        }
        else
        {
            // Default to player 1 if the ball doesn't have a specific player tag
            return "Player1";
        }
    }

    // Function to start the scoring cooldown
    private IEnumerator StartGoalCooldown()
    {
        canScore = false; // Disable scoring
        yield return new WaitForSeconds(goalCooldown); // Wait for the cooldown duration
        canScore = true; // Enable scoring again
    }

    // Function to respawn the ball after a certain delay
    public IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Respawn the ball at its spawn point
        ResetBall();
    }
}
