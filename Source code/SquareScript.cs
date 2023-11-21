using System.Collections.Generic;
using UnityEngine;

public class SquareScript : MonoBehaviour
{
	public AudioClip weakSquareDestroyClip;

	public AudioClip strongSquareDestroyClip;

	public static List<GameObject> squares = new List<GameObject>();

	public int strength;

	private void Start()
	{
		weakSquareDestroyClip = Resources.Load("ZombieHorrorPackageFree/MP3/BodyFall/Foley_BodyFall_001") as AudioClip;
		strongSquareDestroyClip = Resources.Load("AS_Casual_Island_Game_Sounds_FREE/SFX/SFX_1") as AudioClip;
		squares.Add(base.gameObject);
	}

	public static void ClearSquares()
	{
		foreach (GameObject square in squares)
		{
			Object.Destroy(square.gameObject);
		}
		squares.Clear();
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		strength--;
		if (strength == 0)
		{
			GameObject gameObject = null;
			if (base.name.Contains("Strong"))
			{
				gameObject = Object.Instantiate(Resources.Load("ParticleSystems/StrongSquareParticleSystem"), base.transform.position, Quaternion.identity) as GameObject;
				AudioSource.PlayClipAtPoint(strongSquareDestroyClip, base.gameObject.transform.position);
			}
			else if (base.name.Contains("Weak"))
			{
				gameObject = Object.Instantiate(Resources.Load("ParticleSystems/WeakSquareParticleSystem"), base.transform.position, Quaternion.identity) as GameObject;
				AudioSource.PlayClipAtPoint(weakSquareDestroyClip, base.gameObject.transform.position);
			}
			gameObject.GetComponent<ParticleSystem>().Play();
			GameManagerScript.instance.UpdateScore(20);
			Object.Destroy(gameObject, 1f);
			squares.Remove(base.gameObject);
			Object.Destroy(base.gameObject);
		}
		if (strength == 1 && base.gameObject.name.Contains("WeakSquare"))
		{
			squares.Remove(base.gameObject);
			Object.Destroy(base.gameObject);
			(Object.Instantiate(Resources.Load("Squares/WeakSquareCracked"), base.transform.position, Quaternion.identity) as GameObject).GetComponent<SpriteRenderer>().sortingOrder = 1;
		}
		if (strength == 2 && base.gameObject.name.Contains("StrongSquare"))
		{
			squares.Remove(base.gameObject);
			Object.Destroy(base.gameObject);
			(Object.Instantiate(Resources.Load("Squares/StrongSquareCracked"), base.transform.position, Quaternion.identity) as GameObject).GetComponent<SpriteRenderer>().sortingOrder = 1;
		}
		if (squares.Count == 0)
		{
			GameManagerScript.instance.IncrementLevel();
		}
	}
}
