using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float attackCooldown = 1.0f;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private AnimationManager animationManager;
    private PlayerHealth playerHealth;
    private float lastAttackTime;
    public float attackDamage = 10f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        animationManager = GetComponentInChildren<AnimationManager>();

        // Get the PlayerHealth component from the player
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= attackRange)
            {
                AttackPlayer();
                animationManager.SetAttack(true);
            }
            else
            {
                animationManager.SetAttack(false);
                animationManager.SetVelocity(navMeshAgent.velocity.magnitude);
            }
        }
    }

    void AttackPlayer()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            Debug.Log("Attacking the player!");
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
            lastAttackTime = Time.time;
        }
    }
}