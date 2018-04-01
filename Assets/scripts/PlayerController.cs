using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private float currentMoveSpeed;

    private Animator anim;
    private Rigidbody2D myRigidbody;

    private bool playerMoving;
    public Vector2 lastMove;
    private Vector2 moveInput;

    private static bool playerExists;

    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    public string startPoint;
    public bool canMove;

    private SFXManager sfxMan;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        sfxMan = FindObjectOfType<SFXManager>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        canMove = true;
        lastMove = new Vector2(0f, -1f);
    }

    // Get the rigidbody velocity
    Vector2 vel()
    {
        return myRigidbody.velocity;
    }

    // Get the X Input
    float xIn()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    // Get the Y Input
    float yIn()
    {
        return Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        playerMoving = false;
        if (!canMove)
        {
            myRigidbody.velocity = Vector2.zero;
            return;
        }
        if (!attacking)
        {
            float xInput = Input.GetAxisRaw("Horizontal");
            float yInput = Input.GetAxisRaw("Vertical");

            moveInput = new Vector2(xInput, yInput).normalized;
            if (moveInput != Vector2.zero)
            {
                float sprintModifier = 1f;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    sprintModifier = 5f;
                }
                float speedX = moveInput.x * moveSpeed * sprintModifier;
                float speedY = moveInput.y * moveSpeed * sprintModifier;
                myRigidbody.velocity = new Vector2(speedX, speedY);
                playerMoving = true;
                lastMove = moveInput;
            }
            else
            {
                myRigidbody.velocity = Vector2.zero;
            }
        }

        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }
        if (attackTimeCounter <= 0)
        {
            attacking = false;
            anim.SetBool("IsAttacking", false);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            attackTimeCounter = attackTime;
            attacking = true;
            myRigidbody.velocity = Vector2.zero;
            anim.SetBool("IsAttacking", true);

            sfxMan.playerAttack.Play();
        }

        anim.SetFloat("MoveX", xIn());
        anim.SetFloat("MoveY", yIn());
        anim.SetBool("IsMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}