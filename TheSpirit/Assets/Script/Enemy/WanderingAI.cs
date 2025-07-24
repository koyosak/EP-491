using UnityEngine;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] float _speed = 7.0f;
    [SerializeField] float _obstacleRange = 10.0f;

    private readonly float _sphereRadius = 0.75f;

    [SerializeField] GameObject _fireballPrefab;
    [SerializeField] GameObject _fireball;

    private NavMeshAgent _navMeshAgent;
    private Transform _player;

    private bool _isChasing = false;

    private void Awake()
    {
        // Add or retrieve NavMeshAgent
        if (!TryGetComponent(out _navMeshAgent))
        {
            _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }

        // Set the NavMeshAgent speed
        _navMeshAgent.speed = _speed;

        // Disable NavMeshAgent at the start if wandering
        _navMeshAgent.enabled = false;
    }

    private void Start()
    {
        // Find the player by tag (or reference directly if you have a player variable)
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _player = playerObj.transform;
        }
    }

    public void Wander()
    {
        // If chasing, update NavMeshAgent destination
        if (_isChasing)
        {
            if (_player != null)
            {
                _navMeshAgent.SetDestination(_player.position);

                // If close enough, shoot a fireball
                if (!_fireball && Vector3.Distance(transform.position, _player.position) <= _obstacleRange)
                {
                    ShootFireball();
                }
            }
            return; // Skip wandering logic if chasing
        }

        // Wandering logic if not chasing
        transform.Translate(0, 0, _speed * Time.deltaTime);

        Ray ray = new(transform.position, transform.forward);

        if (Physics.SphereCast(ray, _sphereRadius, out RaycastHit hit))
        {
            GameObject hitObj = hit.transform.gameObject;

            // Start chasing if the ray collided with the player
            if (hitObj.GetComponent<CharacterController>() && _player != null)
            {
                StartChase();
            }
            else if (hit.distance < _obstacleRange)
            {
                float theta = Random.Range(-10, 20);
                transform.Rotate(0, theta, 0);
            }
        }
    }

    private void StartChase()
    {
        _isChasing = true;
        _navMeshAgent.enabled = true;
    }

    private void ShootFireball()
    {
        _fireball = Instantiate(
            _fireballPrefab,
            transform.TransformPoint(Vector3.forward * 1.5f),
            transform.rotation);
    }
}
