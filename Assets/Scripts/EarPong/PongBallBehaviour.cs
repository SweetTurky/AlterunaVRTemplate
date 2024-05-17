using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using Alteruna;
public class PongBallBehaviour : AttributesSync
{
    public Transform respawnPointPlayer1; // Set this in the Inspector for Player 1
    public Transform respawnPointPlayer2; // Set this in the Inspector for Player 2
    public float respawnDelay = 2f; // Delay before respawning the ball

    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TableCollider")) // Check if collider around the table is hit
        {
            BroadcastRemoteMethod(nameof(RespawnAfterDelay), respawnDelay);
            return;
        }

        CupBehaviour cup = other.GetComponent<CupBehaviour>();

        if (cup != null)
        {
            if (cup.owner == CupOwner.Player1 && gameObject.CompareTag("Ball1"))
            {
                BroadcastRemoteMethod(nameof(TeleportSphereTo), respawnPointPlayer1);
            }
            else if (cup.owner == CupOwner.Player2 && gameObject.CompareTag("Ball1"))
            {
                BroadcastRemoteMethod(nameof(TeleportSphereTo), respawnPointPlayer1);
            }
            else if (cup.owner == CupOwner.Player1 && gameObject.CompareTag("Ball2"))
            {
                BroadcastRemoteMethod(nameof(TeleportSphereTo), respawnPointPlayer2);
            }
            else if (cup.owner == CupOwner.Player2 && gameObject.CompareTag("Ball2"))
            {
                BroadcastRemoteMethod(nameof(TeleportSphereTo), respawnPointPlayer2);
            }
        }
    }

    // Function to respawn the ball after a certain delay
    [SynchronizableMethod]
    public IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (gameObject.CompareTag("Ball1"))
        {
            TeleportSphereTo(respawnPointPlayer1);
        }
        else if (gameObject.CompareTag("Ball2"))
        {
            TeleportSphereTo(respawnPointPlayer2);
        }
    }

    [SynchronizableMethod]
    public void TeleportSphereTo(Transform targetPosition)
    {
        transform.position = targetPosition.position;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        if (TryGetComponent<XRGrabInteractable>(out var grabInteractable))
        {
            grabInteractable.enabled = true;
        }

        if (TryGetComponent<XRBaseInteractable>(out var baseInteractable))
        {
            baseInteractable.enabled = true;
        }
    }
}
