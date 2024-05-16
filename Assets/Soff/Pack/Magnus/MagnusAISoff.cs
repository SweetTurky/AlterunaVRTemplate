using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagnusAISoff : MonoBehaviour
{
    public NavMeshAgent _agent;
    [SerializeField]
    private Transform[] _waypoints;
    public int _currentWaypointIndex = 0;

    /*[SerializeField]
    private float _walkingDuration = 30f; // Time in seconds for walking
    private float _walkingTimer = 0f; */

    //[SerializeField]
    public Animator _animator;
    public GameObject lookAtObject;

    //public bool _isWalking = false;
    //public float voiceLineDuration = voiceLines[currentVoiceLineIndex].length;
    // Start is called before the first frame update
    public AudioSource magnusAudioSource;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("Nav Mesh Agent is Null.");
        }
        //SetNextWaypoint();
    }

    // Update is called once per frame
    /*void Update()
    {
        if (_isWalking)
        {
            if (!_agent.pathPending && _agent.remainingDistance > _agent.stoppingDistance)
            {
                // Play the walking animation
                if (_animator != null)
                {
                    _animator.SetBool("IsWalking", true);
                    _animator.SetBool("IsTalking", false); // Make sure talking is false when walking
                }
            }
            else
            {
                // Stop the walking animation
                if (_animator != null)
                {
                    _animator.SetBool("IsWalking", false);
                    _animator.SetBool("IsTalking", true); // Start talking animation
                }
            }
        }
        else
        {
            // Play the talking animation when not walking
            if (_animator != null)
            {
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsTalking", false);
            }
        }
    }*/

    public void SetNextWaypoint()
    {
        if (_waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned.");
            return;
        }

        if (_currentWaypointIndex + 1 == 3)
        {
            _currentWaypointIndex = 0;
        }

        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        _agent.SetDestination(_waypoints[_currentWaypointIndex].position);
    }

}


