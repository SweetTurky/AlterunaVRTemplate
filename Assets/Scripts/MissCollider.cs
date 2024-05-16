using UnityEngine;
using System.Collections;

public class MissCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entered object is a basketball
        if (other.CompareTag("Player1Basketball") || other.CompareTag("Player2Basketball"))
        {
            // Get the BasketballBehaviour component attached to the basketball GameObject
            BasketballBehaviour basketballBehaviour = other.GetComponent<BasketballBehaviour>();
            
            // Check if the basketballBehaviour is not null
            if (basketballBehaviour != null)
            {
                // Check if the ball is not scored
                if (!basketballBehaviour.isScored)
                {
                    // The ball missed, trigger respawn logic
                    Debug.Log("You missed, resetting the ball");
                    basketballBehaviour.StartCoroutine(basketballBehaviour.RespawnAfterDelay(2f));
                }
            }
            else
            {
                Debug.LogError("BasketballBehaviour component not found on the basketball GameObject.");
            }
        }
    }

}
