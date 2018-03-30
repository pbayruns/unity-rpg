using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private float currentMoveSpeed;
    public float diagonalMoveModifier;

    private Animator anim;
    private Rigidbody2D myRigidbody;

    private bool playerMoving;
    public Vector2 lastMove;

    private static bool playerExists;

    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    public string startPoint;
    public bool canMove;
    // Use this for initialization
    void Start()
    {
        canMove = true;
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
            if (xIn() > 0.5f || xIn() < -0.5f)
            {
                //transform.Translate(new Vector3(horiz * moveSpeed * Time.deltaTime, 0f, 0f));
                myRigidbody.velocity = new Vector2(xIn() * currentMoveSpeed, vel().y);
                lastMove = new Vector2(xIn(), 0f);
                playerMoving = true;
            }
            if (yIn() > 0.5f || yIn() < -0.5f)
            {
                //transform.Translate(new Vector3(0f, vert * moveSpeed * Time.deltaTime, 0f));
                myRigidbody.velocity = new Vector2(vel().x, yIn() * currentMoveSpeed);
                lastMove = new Vector2(0f, yIn());
                playerMoving = true;
            }

            if (xIn() < 0.5f && xIn() > -0.5f)
            {
                myRigidbody.velocity = new Vector2(0f, vel().y);
            }
            if (yIn() < 0.5f && yIn() > -0.5f)
            {
                myRigidbody.velocity = new Vector2(vel().x, 0f);
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
        }

        if(Mathf.Abs(xIn()) > 0.5f && Mathf.Abs(yIn()) > 0.5f)
        {
            currentMoveSpeed = moveSpeed * diagonalMoveModifier;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }
        anim.SetFloat("MoveX", xIn());
        anim.SetFloat("MoveY", yIn());
        anim.SetBool("IsMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}