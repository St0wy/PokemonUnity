using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
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
	private float t;
	private bool isMoving;
	private int inputCounter;


	// Start is called before the first frame update
	void Start()
	{
		sp = GetComponent<SpriteRenderer>();
		pos = transform.position;
		input = new Vector2(0, 1);
		speed = 4f;
		GetComponent<Animator>().SetFloat("MouvementSpeed", speed);
		t = 0f;
		isMoving = false;

		facing = Direction.Up;
		oldInput = Direction.None;
		inputCounter = 0;
	}

	void Update()
	{

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

		if (!isMoving)
		{
            //Get the input
			input = new Vector2(Input.GetAxis(AXIS_HORIZONTAL), Input.GetAxis(AXIS_VERTICAL));
			if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
			{
				input.y = 0;
			}
			else
			{
				input.x = 0;
			}

            //Sprite management
			switch (facing)
			{
				case Direction.Up:
					sp.sprite = sprIdleUp;
					break;
				case Direction.Down:
					sp.sprite = sprIdleDown;
					break;
				case Direction.Left:
					sp.sprite = sprIdleSide;
					break;
				case Direction.Right:
					sp.sprite = sprIdleSide;
					break;
				default:
					break;
			}

            //Move
			if (input != Vector2.zero)
			{
				StartCoroutine(Move(transform));
			}
		}
		else
		{
            //Sprite management
			switch (facing)
			{
				case Direction.Up:
					if (t <= 1f / 6f)
					{
						sp.sprite = sprIdleUp;
					}
					else if (t <= (1f / 3f) * 2.5f)
					{
						sp.sprite = sprMovingUp;
					}
					else if (t <= 1f)
					{
						sp.sprite = sprIdleUp;
					}
					break;
				case Direction.Down:
					if (t <= 1f / 6f)
					{
						sp.sprite = sprIdleDown;
					}
					else if (t <= (1f / 3f) * 2.5f)
					{
						sp.sprite = sprMovingDown;
					}
					else if (t <= 1f)
					{
						sp.sprite = sprIdleDown;
					}
					break;
				case Direction.Left:
					if (t <= 1f / 6f)
					{
						sp.sprite = sprIdleSide;
					}
					else if (t <= (1f / 3f) * 2.5f)
					{
						sp.sprite = sprMovingSide;
					}
					else if (t <= 1f)
					{
						sp.sprite = sprIdleSide;
					}
					break;
				case Direction.Right:
					if (t <= 1f / 6f)
					{
						sp.sprite = sprIdleSide;
					}
					else if (t <= (1f / 3f) * 2.5f)
					{
						sp.sprite = sprMovingSide;
					}
					else if (t <= 1f)
					{
						sp.sprite = sprIdleSide;
					}
					break;
				default:
					break;
			}
		}

		//Sets the parameters for the animator
		GetComponent<Animator>().SetFloat("Speed", t);
		GetComponent<Animator>().SetInteger("Facing", (int)facing);
	}

	/// <summary>
	/// Moves the player on a grid
	/// </summary>
	/// <param name="transform">transform of the player you want to move.</param>
	/// <returns>mdr je sais pas</returns>
	public IEnumerator Move(Transform transform)
	{
		isMoving = true;
		Vector3 startPosition = transform.position;
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
			Debug.Log(t.ToString()); t += Time.deltaTime * (speed);
			transform.position = Vector3.Lerp(startPosition, endPosition, t);

			yield return null;
		}
		t = 0f;
		isMoving = false;
		yield return 0;
	}
}
