using System.Collections;
using UnityEngine;

class GridMove : MonoBehaviour
{
    private Animator anim;
    private float moveSpeed = 3f;
    private float gridSize = 1f;
    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Vertical;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;

    public void Start()
    {
        this.anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (!isMoving)
        {
            float MoveX = Input.GetAxis("Horizontal");
            float MoveY = Input.GetAxis("Vertical");
            anim.SetFloat("MoveX", MoveX);
            anim.SetFloat("MoveY", MoveY);
            input = new Vector2(MoveX, MoveY);


            if (!allowDiagonals)
            {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                {
                    input.y = 0;
                }
                else
                {
                    input.x = 0;
                }
            }

            if (input != Vector2.zero)
            {
                StartCoroutine(move(transform));
            }
        }
    }

    public IEnumerator move(Transform transform)
    {
        isMoving = true;
        anim.SetBool("IsMoving", isMoving);

        startPosition = transform.position;
        t = 0;

        if (gridOrientation == Orientation.Horizontal)
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);
        }
        else
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }
        anim.SetFloat("LastMoveX", input.x);
        anim.SetFloat("LastMoveY", input.y);
        if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
        {
            factor = 0.7071f;
        }
        else
        {
            factor = 1f;
        }

        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        isMoving = false;
        anim.SetBool("IsMoving", isMoving);
        yield return 0;
    }
}