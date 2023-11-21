using UnityEngine;

public class SpeedCorrectionScript : MonoBehaviour
{
	public Rigidbody2D rb;

	private void FixedUpdate()
	{
		Vector3 vector = rb.velocity;
		if (vector.magnitude < 3f)
		{
			rb.velocity *= (Vector2)(vector.normalized * 4f);
		}
	}
}
