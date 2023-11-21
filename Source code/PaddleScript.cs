using UnityEngine;

public class PaddleScript : MonoBehaviour
{
	private void Update()
	{
		MovePaddle();
	}

	public void resizePaddle(float resizeFactor)
	{
		Vector3 localScale = base.transform.localScale;
		base.transform.localScale = new Vector3(localScale.x * resizeFactor, localScale.y, localScale.z);
	}

	private void MovePaddle()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		position.z = 0f;
		position.y = -4.5f;
		base.transform.position = position;
		Vector3 position2 = base.transform.position;
		if (position2.x > 2.45f)
		{
			position2.x = 2.45f;
			base.transform.position = position2;
		}
		if (position2.x < -2.45f)
		{
			position2.x = -2.45f;
			base.transform.position = position2;
		}
	}
}
