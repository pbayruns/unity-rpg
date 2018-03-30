using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMovement : MonoBehaviour {

    public float moveSpeed;
    public bool isWalking;

    public float walkTime;
    public float waitTime;

    public Collider2D walkZone;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    private bool hasWalkZone;

    private float walkCounter;
    private float waitCounter;

    private Rigidbody2D myRigidbody;
    private int walkDirection;

    public bool canMove;
    private DialogueManager dialogueMan;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        walkCounter = walkTime;

        dialogueMan = FindObjectOfType<DialogueManager>();

        if (walkZone != null)
        {
            hasWalkZone = true;
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!dialogueMan.dialogueActive)
        {
            canMove = true;
        }

        if (!canMove)
        {
            myRigidbody.velocity = Vector2.zero;
            return;
        }
        if (isWalking)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    myRigidbody.velocity = new Vector2(0f, moveSpeed);
                    if (hasWalkZone && transform.position.y > maxWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 1:
                    myRigidbody.velocity = new Vector2(moveSpeed, 0f);
                    if (hasWalkZone && transform.position.x > maxWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 2:
                    myRigidbody.velocity = new Vector2(0f, -moveSpeed);
                    if (hasWalkZone && transform.position.y < minWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 3:
                    myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
                    if (hasWalkZone && transform.position.x < minWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
            }

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            myRigidbody.velocity = Vector2.zero;
            if(waitCounter < 0)
            {
                chooseDirection();
            }
        }
	}

    public void chooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NPCMovement : MonoBehaviour
//{

//    public float moveSpeed;
//    private Rigidbody2D rb;
//    public bool isWalking;

//    public Vector2 facing;

//    public float walkTime;
//    private float walkCounter;
//    public float waitTime;
//    private float waitCounter;

//    private int walkDirection;

//    public Animator anim;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        anim = GetComponent<Animator>();
//        waitCounter = waitTime;
//        walkCounter = walkTime;

//        ChooseDirection();

//    }


//    void Update()
//    {
//        if (isWalking == false)
//        {
//            facing.x = 0;
//            facing.y = 0;
//        }

//        if (isWalking == true)
//        {
//            walkCounter -= Time.deltaTime;


//            switch (walkDirection)
//            {


//                case 0:
//                    rb.velocity = new Vector2(0, moveSpeed);
//                    facing.y = 1;
//                    facing.x = 0;
//                    break;

//                case 1:
//                    rb.velocity = new Vector2(moveSpeed, 0);
//                    facing.x = 1;
//                    facing.y = 0;
//                    transform.localScale = new Vector3(6f, 6f, 1f);
//                    break;

//                case 2:
//                    rb.velocity = new Vector2(0, -moveSpeed);
//                    facing.y = -1;
//                    facing.x = 0;
//                    break;

//                case 3:
//                    rb.velocity = new Vector2(-moveSpeed, 0);
//                    facing.y = 0;
//                    facing.x = -1;
//                    transform.localScale = new Vector3(-6f, 6f, 1f);

//                    break;
//            }

//            if (walkCounter < 0)
//            {
//                isWalking = false;
//                waitCounter = waitTime;
//            }
//        }

//        else
//        {
//            rb.velocity = Vector2.zero;

//            waitCounter -= Time.deltaTime;

//            if (waitCounter < 0)
//            {
//                ChooseDirection();

//            }
//        }

//        anim.SetFloat("YourFloat", facing.x);
//        anim.SetFloat("YourFloat", facing.y);
//        anim.SetBool("YourBool", isWalking);
//    }

//    public void ChooseDirection()
//    {
//        walkDirection = Random.Range(0, 4);
//        isWalking = true;
//        walkCounter = walkTime;
//    }
//}