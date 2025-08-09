using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsPlayer;
    public float health;


    [Header("Patrolling Settings")]
    [SerializeField] private float forwardSpeed = 2.0f;
    [SerializeField] private float obstacleRange = 10.0f;
    private float patrolTurnCooldown = 2f;
    private float lastTurnTime = 0f;

    // Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject projectile;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("FirstPersonController").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for player proximity
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        // Stop NavMeshAgent movement to avoid conflict with manual movement
        if (agent.enabled)
            agent.ResetPath();

        // Move forward
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Detect obstacles with SphereCast
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.SphereCast(ray, 0.75f, out RaycastHit hit, obstacleRange))
        {
            // Only react to non-player obstacles
            if (hit.collider.gameObject != player.gameObject)
            {
                if (Time.time - lastTurnTime > patrolTurnCooldown)
                {
                    float turnAngle = Random.Range(-110f, 110f);
                    transform.Rotate(0, turnAngle, 0);
                    lastTurnTime = Time.time;
                }
            }
        }
    }

    private void ChasePlayer()
    {
        if (!agent.enabled)
            return;

        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        if (!agent.enabled)
            return;

        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Shoot projectile
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}

