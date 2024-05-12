using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent _agent;
    [SerializeField]
    private Transform[] _waypoints;
    private int _currentWaypointIndex = 0;

    [SerializeField]
    private float _walkingDuration = 30f; // Time in seconds for walking
    private float _walkingTimer = 0f;

    [SerializeField]
    private Animator _animator;

    private bool _isWalking = true;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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

            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                SetNextWaypoint();
            }

            if (_walkingTimer >= _walkingDuration)
            {
                _isWalking = false;
                if (_animator != null)
                {
                    _animator.SetBool("IsWalking", false);
                    _animator.SetTrigger("Talking");
                }
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
}
