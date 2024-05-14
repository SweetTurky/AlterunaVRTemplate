using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagnusAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]
    private Transform[] _waypoints;
    private int _currentWaypointIndex = 0;

    [SerializeField]
    private float _walkingDuration = 30f; // Time in seconds for walking
    private float _walkingTimer = 0f;

    [SerializeField]
    private Animator _animator;

    public GameObject lookAtObject;

    private bool _isWalking = true;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("Nav Mesh Agent is Null.");
        }

        SetNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isWalking)
        {
            _walkingTimer += Time.deltaTime;

            if (!_agent.pathPending && _agent.remainingDistance < 0.1f)
            {
                SetNextWaypoint();
            }

            if (_walkingTimer >= _walkingDuration)
            {
                StartCoroutine(HandleTalkingRoutine());
            }
        }
    }

    private void SetNextWaypoint()
    {
        if (_waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned.");
            return;
        }

        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        _agent.SetDestination(_waypoints[_currentWaypointIndex].position);
    }

    private IEnumerator HandleTalkingRoutine()
    {
        _isWalking = false;
        _walkingTimer = 0f;
        _agent.isStopped = true; // Stop the agent from moving

        if (_animator != null)
        {
            _animator.SetBool("IsWalking", false);
            _animator.SetTrigger("Talking");
            transform.LookAt(lookAtObject.transform);
        }

        // Assuming talking animation length is 5 seconds. Adjust based on your animation length.
        yield return new WaitForSeconds(5f); 

        if (_animator != null)
        {
            _animator.ResetTrigger("Talking");
            _animator.SetBool("IsWalking", true);
        }

        _agent.ResetPath(); // Clear any residual paths
        yield return new WaitForEndOfFrame(); // Ensure path reset takes effect
        
        _agent.isStopped = false; // Resume the agent's movement
        //SetNextWaypoint(); // Reassign the next waypoint
        
        _isWalking = true;
        
    }
}
