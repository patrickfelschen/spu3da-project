using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;

    public LayerMask enemyLayer;

    public LayerMask itemLayer;

    public Animator animator;

    private Transform cameraTransform;

    public Interactable focus;

    public float turnSmoothTime = 0.1f;

    private float moveSpeed;

    private float turnSmoothVelocity;

    private bool isBlocking = false;

    private float attackTimer = 0;

    private bool walkAudioPlays = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Main Logic").GetComponent<GameLogic>().isMenuOpen) { return; }

        attackTimer += Time.deltaTime;

        Move();

        Block();

        ItemInteract();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackEnemy();
        }
        if (focus != null && Input.GetKeyDown(KeyCode.E))
        {
            focus.GetComponent<Chest>().GetLoot();
        }
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            if(!PlayerManager.instance.GetInventory().IsEmpty())
            {
                PlayerManager.instance.GetInventory().GetHpItem().Consume();
            }
        }
    }

    private void Move()
    {
        // Eingabe einlesen
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Richtung der Bewegung
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool isMoving = direction != Vector3.zero;

        if (characterController.isGrounded && !isBlocking)
        {
            // Berechnung der Rotation damit Player in die Bewegungsrichtung schaut
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            // weiche Bewegung berechnen
            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothVelocity,
                turnSmoothTime
            );

            if (isMoving && !Input.GetKey(KeyCode.LeftShift))
            {
                // Walk
                Walk();
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                if (!AudioManager.instance.IsPlaying("PlayerWalk"))
                {
                    AudioManager.instance.Play("PlayerWalk");
                }
            }
            else if (isMoving && Input.GetKey(KeyCode.LeftShift))
            {
                // Run
                Run();
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            else if (!isMoving)
            {
                // Idle
                Idle();
                AudioManager.instance.Stop("PlayerWalk");
            }

            // Player bewegen
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

        }


    }

    private void Idle() {
        animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
        moveSpeed = 0;
    }

    private void Walk() {
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
        moveSpeed = PlayerManager.instance.GetPlayerStats().walkSpeed.GetValue();
    }

    private void Run() {
        animator.SetFloat("Speed", 1.0f, 0.1f, Time.deltaTime);
        moveSpeed = PlayerManager.instance.GetPlayerStats().runSpeed.GetValue();
    }

    private void AttackEnemy()
    {
        if(attackTimer < PlayerManager.instance.GetPlayerStats().attackSpeed.GetValue()) { return; }
        if (isBlocking) return;

        animator.SetTrigger("Attack");
        attackTimer = 0;

        AudioManager.instance.Play("Attack");

        Collider[] collider = Physics.OverlapSphere(transform.position, 3, enemyLayer);

        foreach(Collider c in collider)
        {
            Interactable i = c.gameObject.GetComponent<Interactable>();
            if (i != null)
            {
                i.Interact();
            }
        }
    }

    private void Block()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isBlocking)
        {
            animator.SetTrigger("EnterBlock");
            AudioManager.instance.Stop("PlayerWalk");
            GetComponent<PlayerStats>().DoBlock();
            isBlocking = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) && isBlocking)
        {
            animator.SetTrigger("ExitBlock");
            GetComponent<PlayerStats>().ExitBlock();
            isBlocking = false;
        }
        
    }

    private void ItemInteract()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 1.5f, itemLayer);

        if(collider.Length > 0)
        {
            Interactable i = collider[0].gameObject.GetComponent<Interactable>();
            if (i != null)
            {
                Debug.Log("CHEST INTERACT");
                i.OnFocused(transform);
                i.Interact();
                focus = i;
            }
        }
        else
        {
            focus = null;
        }
    }

}


//THIRD PERSON MOVEMENT in Unity https://www.youtube.com/watch?v=4HpC--2iowE
//Sword effect  https://www.youtube.com/watch?v=c8hijUge7IY