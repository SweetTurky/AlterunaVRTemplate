using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagnusAISoff : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]
    private Transform[] _waypoints;
    public int _currentWaypointIndex = 0;

    [SerializeField]
    private float _walkingDuration = 30f; // Time in seconds for walking
    private float _walkingTimer = 0f;

    //[SerializeField]
    private Animator _animator;
    public GameObject lookAtObject;

    private bool _isWalking = true;
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

        SetNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isWalking)
        {
            _walkingTimer += Time.deltaTime;

            if (!_agent.pathPending  &&  _agent.remainingDistance < 0.5f)
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

        if(_currentWaypointIndex + 1 == 5)
        {
            _currentWaypointIndex = 0;
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
        yield return new WaitForSeconds(2f); 

        if (_animator != null)
        {
            _animator.ResetTrigger("Talking");
            _animator.SetBool("IsWalking", true);
            transform.LookAt(_waypoints[_currentWaypointIndex].transform);
        }

        _agent.ResetPath(); // Clear any residual paths
        
        _agent.isStopped = false; // Resume the agent's movement
        
        _isWalking = true;
        yield return new WaitForEndOfFrame(); // Ensure path reset takes effect
    }
    
}


