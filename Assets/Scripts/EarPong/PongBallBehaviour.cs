using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallBehaviour : MonoBehaviour
{
    public Transform respawnPointPlayer1; // Set this in the Inspector for Player 1
    public Transform respawnPointPlayer2; // Set this in the Inspector for Player 2

    void OnTriggerEnter(Collider other)
    {
        CupBehaviour cup = other.GetComponent<CupBehaviour>();

        if (cup != null)
        {
            if (cup.owner == CupOwner.Player1)
            {
                TeleportSphereTo(respawnPointPlayer1);
            }
            else if (cup.owner == CupOwner.Player2)
            {
                TeleportSphereTo(respawnPointPlayer2);
            }
        }
    }

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
