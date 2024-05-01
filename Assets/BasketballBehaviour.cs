using System.Collections;
using UnityEngine;

public class BasketballBehaviour : MonoBehaviour
{
    public Transform spawnPoint; // Spawn point for the ball
    public BasketballManager basketballManager; // Reference to the BasketballManager script
    public bool isScored = false;
    private Rigidbody rb;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the BasketballManager script attached
        if (other.gameObject.GetComponent<BasketballManager>() != null)
        {
            // Call ScoredGoal method of BasketballManager
            basketballManager.ScoredGoal();
            isScored = true;
            Debug.Log("You scored!");

            // Reset the ball to its spawn point
            StartCoroutine(RespawnAfterDelay(2f));
        }
    }

    // Function to respawn the ball after a certain delay
    public IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Respawn the ball at its spawn point
        ResetBall();
    }
}
