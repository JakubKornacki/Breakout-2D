using UnityEngine;

public class BallScript : MonoBehaviour
{
	public AudioSource collisionAudio;

	public AudioSource shootBallAudio;

	public Rigidbody2D rb;

	[HideInInspector]
	public int ballState;

	public GameObject paddle;

	[HideInInspector]
	private Vector3 leftDirection = new Vector3(-1f, 1f, 0f);

	[HideInInspector]
	private Vector3 rightDirection = new Vector3(1f, 1f, 0f);

	private void Start()
	{
		paddle = GameObject.Find("Paddle(Clone)");
	}

	private void Update()
	{
		if (ballState == 0)
		{
			Vector3 position = paddle.transform.position;
			base.transform.position = new Vector3(position.x, -4.2f, 0f);
		}
		if (Input.GetKey(KeyCode.Mouse0) && ballState == 0 && GameManagerScript.instance.GetState() == GameManagerScript.GAMESTATES.GAME)
		{
			Vector3 vector = ((Random.Range(0f, 1f) < 0.5f) ? leftDirection : rightDirection);
			rb.AddForce(150f * vector);
			ballState = 1;
			shootBallAudio.Play();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		collisionAudio.Play();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		ballState = 0;
		rb.velocity = Vector2.zero;
		GameManagerScript.instance.DecrementLives();
	}
}
