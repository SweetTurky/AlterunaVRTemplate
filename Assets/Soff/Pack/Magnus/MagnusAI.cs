using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagnusAI : MonoBehaviour
{
    public Transform walkPoint1; // First walk point
    public Transform walkPoint2; // Second walk point
    public Transform walkPoint3;
    public Transform walkPoint4;
    public AudioClip[] voiceLines; // Array of voice lines to play
    public AudioSource magnusAudioSource;
    private int currentVoiceLineIndex = 0; // Index of the current voice line

    private bool isPlayingVoiceLine = false; // Flag to indicate if NPC is currently playing a voice line

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Start walking when the game starts
        StartCoroutine(WalkBetweenPoints());
    }

    IEnumerator WalkBetweenPoints()
    {
        while (true)
        {
            // Walk to the first walk point
            yield return WalkTo(walkPoint1.position);

            // Stand still and play voice line
            yield return PlayVoiceLine();

            // Walk to the second walk point
            yield return WalkTo(walkPoint2.position);

            // Stand still and play voice line
            yield return PlayVoiceLine();
        }
    }

    IEnumerator WalkTo(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
        animator.SetBool("isWalking", true);
        while (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            yield return null;
        }
        animator.SetBool("isWalking", false);
    }

    IEnumerator PlayVoiceLine()
    {
        isPlayingVoiceLine = true;
        // Play voice line here
        yield return new WaitForSeconds(2); // Placeholder for voice line duration
        isPlayingVoiceLine = false;
        currentVoiceLineIndex = (currentVoiceLineIndex + 1) % voiceLines.Length; // Move to the next voice line in the array
    }
}
