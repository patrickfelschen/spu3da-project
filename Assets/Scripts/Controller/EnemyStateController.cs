using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyStateController : MonoBehaviour
{
    public float chaseRange;
    public float attackRange;
    public float walkPointRange;
    public LayerMask whatIsGround;

    private Animator animator;
    private NavMeshAgent agent;
    private CharacterCombat combat;

    enum State { isIdling, isPatroling, isChasing, isAttacking };
    private State state;

    private Vector3 nextWalkPoint;
    private bool walkPointSet;

    private float randomIdleTime;
    private float randomPatrolingTime;

    private float timer;
    private float attackTimer;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
        timer = 0;
        state = State.isIdling;

        randomIdleTime = Random.Range(3, 6);
        randomPatrolingTime = Random.Range(6, 12);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        if (GameObject.FindGameObjectWithTag("Main Logic").GetComponent<GameLogic>().isMenuOpen) { return; }

        timer += Time.deltaTime;
        attackTimer += Time.deltaTime;

        // 5 sec idling
        if (state == State.isIdling && timer > randomIdleTime)
        {
            timer = 0;
            state = State.isPatroling;
        }

        // 10 sec patroling
        if (state == State.isPatroling && timer > randomPatrolingTime)
        {
            timer = 0;
            state = State.isIdling;
        }

        float distance = Vector3.Distance(PlayerManager.instance.player.transform.position, transform.position);

        // angreifen
        if (distance < attackRange)
        {
            state = State.isAttacking;
        }
        // verfolgen
        else if (distance < chaseRange)
        {
            state = State.isChasing;
        }
        else if (state != State.isIdling)
        {
            state = State.isPatroling;
        }

        // states starten
        if (state == State.isIdling)
        {
            IdleState();
        }
        else if (state == State.isPatroling)
        {
            PatrolState();
        }
        else if (state == State.isChasing)
        {
            ChaseState();
        }
        else if (state == State.isAttacking)
        {
            AttackState();
        }
    }

    private void IdleState()
    {
        if (agent.isOnNavMesh)
        {
            animator.SetFloat("Speed", 0);
            agent.SetDestination(agent.transform.position);
        }
            
    }

    private void PatrolState()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            if (agent.isOnNavMesh)
            {
                agent.SetDestination(nextWalkPoint);
                animator.SetFloat("Speed", 1);

                if (agent.remainingDistance < 0.3f)
                {
                    walkPointSet = false;
                }
            }
        }
    }

    private void ChaseState()
    {
        if (agent.isOnNavMesh)
        {
            animator.SetFloat("Speed", 1);
            agent.SetDestination(PlayerManager.instance.player.transform.position);
        }
    }

    private void AttackState()
    {
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(agent.transform.position);
        }
        animator.SetFloat("Speed", 0);
        transform.LookAt(PlayerManager.instance.transform);

        if (attackTimer < GetComponent<CharacterStats>().attackSpeed.GetValue()) { return; }

        
        // Attack code
        animator.SetTrigger("Attack");

        CharacterStats targetStats = PlayerManager.instance.transform.GetComponent<CharacterStats>();

        if (targetStats != null)
        {
            PlayHitSound();
            combat.Attack(targetStats);
        }
        
        attackTimer = 0;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        nextWalkPoint = new Vector3(
            agent.transform.position.x + randomX,
            agent.transform.position.y,
            agent.transform.position.z + randomZ
        );

        // Check if walkpoint is on ground
        if (Physics.Raycast(nextWalkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            Debug.Log("FOUND");
        }
        else
        {
            Debug.Log("OUTSIDE");
        }
    }

    public virtual void PlayHitSound() 
    {
        AudioManager.instance.Play("Attack");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);

    }
}
