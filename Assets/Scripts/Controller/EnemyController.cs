using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    private CharacterCombat combat;

    private Animator animator;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 5;

    //Attacking
    public float timeBetweenAttacks = 2f;
    bool alreadyAttacked;

    //States
    public float sightRange = 10f;
    public float attackRange = 0.1f;
    public bool playerInSightRange, playerInAttackRange;

    public bool isDead = false;

    private float attackTimer = 0;

    private void Awake()
    {
        //player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();

        player = PlayerManager.instance.transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Main Logic").GetComponent<GameLogic>().isMenuOpen) 
        {
            StopMoving();
            return; 
        }

        attackTimer += Time.deltaTime;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (!isDead)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 0.2f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(
            agent.transform.position.x + randomX,
            agent.transform.position.y,
            agent.transform.position.z + randomZ
        );

        // Check if walkpoint is on ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            Debug.Log("FOUND");
        }
        else
        {
            Debug.Log("OUTSIDE");
        }
    }

    public void StopMoving()
    {
        agent.SetDestination(agent.transform.position);
    }

    private void ChasePlayer()
    {
        agent.SetDestination(PlayerManager.instance.transform.position);
    }

    private void AttackPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Main Logic").GetComponent<GameLogic>().isMenuOpen) { return; }
        if (attackTimer < gameObject.GetComponent<CharacterStats>().attackSpeed.GetValue()) { return; }

        StopMoving();

        transform.LookAt(PlayerManager.instance.transform);

        if (!alreadyAttacked)
        {
            // Attack code

            //Debug.Log("Attack");
            animator.SetTrigger("Attack");

            CharacterStats targetStats = PlayerManager.instance.transform.GetComponent<CharacterStats>();

            if (targetStats != null)
            {
                AudioManager.instance.Play("Attack");
                combat.Attack(targetStats);
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

// FULL 3D ENEMY AI in 6 MINUTES! || Unity Tutorial https://www.youtube.com/watch?v=UjkSFoLxesw