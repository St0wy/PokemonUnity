using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    private const string AXIS_HORIZONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";
    private const int MAX_INPUT_COUNTER = 5;

    //private enum State { IdleUp, IdleDown, IdleLeft, IdleRight, MovingUp, MovingDown, MovingLeft, MovingRight};
    private enum Direction { Up, Down, Left, Right, None };

    [SerializeField]
    private Sprite sprIdleUp;
    [SerializeField]
    private Sprite sprIdleDown;
    [SerializeField]
    private Sprite sprIdleSide;

    [SerializeField]
    private Sprite sprMovingUp;
    [SerializeField]
    private Sprite sprMovingDown;
    [SerializeField]
    private Sprite sprMovingSide;

    private SpriteRenderer sp;
    private Vector3 pos;
    private Vector2 input;
    private Direction facing;
    private Direction oldInput;
    private float speed;
    private bool isMoving;
    private int inputCounter;
    

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        pos = transform.position;
        input = new Vector2(0, 1);
        speed = 5f;
        isMoving = false;

        facing = Direction.Up;
        oldInput = Direction.None;
        inputCounter = 0;
    }

    void FixedUpdate()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis(AXIS_HORIZONTAL), Input.GetAxis(AXIS_VERTICAL));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.y = 0;
            }
            else
            {
                input.x = 0;
            }

            switch (facing)
            {
                case Direction.Up:
                    sp.sprite = sprIdleUp;
                    sp.flipX = false;
                    break;
                case Direction.Down:
                    sp.sprite = sprIdleDown;
                    sp.flipX = false;
                    break;
                case Direction.Left:
                    sp.sprite = sprIdleSide;
                    sp.flipX = false;
                    break;
                case Direction.Right:
                    sp.sprite = sprIdleSide;
                    sp.flipX = true;
                    break;
                default:
                    break;
            }

            if (input != Vector2.zero)
            {
                StartCoroutine(Move(transform));
            }
        }
        else
        {
            switch (facing)
            {
                case Direction.Up:
                    sp.sprite = sprMovingUp;
                    sp.flipX = false;
                    break;
                case Direction.Down:
                    sp.sprite = sprMovingDown;
                    sp.flipX = false;
                    break;
                case Direction.Left:
                    sp.sprite = sprMovingSide;
                    sp.flipX = false;
                    break;
                case Direction.Right:
                    sp.sprite = sprMovingSide;
                    sp.flipX = true;
                    break;
                default:
                    break;
            }
        }
    }

    public IEnumerator Move(Transform transform)
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        float t = 0f;
        Vector3 endPosition = startPosition;

        if (input.y < 0) //Down
        {
            endPosition += Vector3.down;
            facing = Direction.Down;
        }
        else if (input.y > 0) //Up
        {
            endPosition += Vector3.up;
            facing = Direction.Up;
        }
        if (input.x < 0) //Left
        {
            endPosition += Vector3.left;
            facing = Direction.Left;
        }
        else if (input.x > 0) //Right
        {
            endPosition += Vector3.right;
            facing = Direction.Right;
        }

        while (t < 1f)
        {
            Debug.Log(t.ToString());
            t += Time.deltaTime * (speed);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        isMoving = false;
        yield return 0;
    }
}
