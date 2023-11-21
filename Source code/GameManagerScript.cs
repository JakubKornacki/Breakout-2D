using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	public enum GAMESTATES
	{
		MENU,
		GAME,
		GAMEOVER,
		VICTORY
	}

	public static GameManagerScript instance;

	private GAMESTATES gameState;

	public GameObject menuCanvas;

	public GameObject gameCanvas;

	public GameObject gameOverCanvas;

	public GameObject victoryCanvas;

	private GameObject currentLevel;

	public TMP_Text txtScore;

	public TMP_Text txtBrickCount;

	public TMP_Text txtLives;

	private int score;

	private int targetCount;

	private int lives = 3;

	private int level = 1;

	public GameObject paddle;

	private void Start()
	{
		instance = this;
		instance.gameCanvas.SetActive(value: true);
		SetGameState(GAMESTATES.MENU);
	}

	public GAMESTATES GetState()
	{
		return gameState;
	}

	public void DecrementLives()
	{
		lives--;
		if (lives == 0)
		{
			SetGameState(GAMESTATES.GAMEOVER);
			SquareScript.ClearSquares();
		}
	}

	public void Replay()
	{
		SetLevel(1);
		SetLives(3);
		SetScore(0);
		paddle.GetComponent<PaddleScript>().resizePaddle(0.95f);
		SetGameState(GAMESTATES.GAME);
	}

	private void SetLevel(int level)
	{
		if (currentLevel != null)
		{
			Object.Destroy(currentLevel);
		}
		paddle.GetComponent<PaddleScript>().resizePaddle(0.95f);
		switch (level)
		{
		case 1:
			currentLevel = Object.Instantiate(Resources.Load("Levels/Level1")) as GameObject;
			break;
		case 2:
			currentLevel = Object.Instantiate(Resources.Load("Levels/Level2")) as GameObject;
			break;
		case 3:
			currentLevel = Object.Instantiate(Resources.Load("Levels/Level3")) as GameObject;
			break;
		case 4:
			currentLevel = Object.Instantiate(Resources.Load("Levels/Level4")) as GameObject;
			break;
		case 5:
			currentLevel = Object.Instantiate(Resources.Load("Levels/Level5")) as GameObject;
			break;
		case 6:
			currentLevel = Object.Instantiate(Resources.Load("Levels/Level6")) as GameObject;
			break;
		}
		if (currentLevel != null)
		{
			targetCount = currentLevel.transform.childCount;
		}
	}

	public void SetGameState(GAMESTATES state)
	{
		gameState = state;
		if (gameState == GAMESTATES.MENU)
		{
			instance.menuCanvas.SetActive(value: true);
			instance.gameCanvas.SetActive(value: false);
			instance.gameOverCanvas.SetActive(value: false);
			instance.victoryCanvas.SetActive(value: false);
		}
		if (gameState == GAMESTATES.GAME)
		{
			instance.menuCanvas.SetActive(value: false);
			instance.gameCanvas.SetActive(value: true);
			instance.gameOverCanvas.SetActive(value: false);
			instance.victoryCanvas.SetActive(value: false);
		}
		if (gameState == GAMESTATES.GAMEOVER)
		{
			instance.gameCanvas.SetActive(value: false);
			instance.menuCanvas.SetActive(value: false);
			instance.gameOverCanvas.SetActive(value: true);
			instance.victoryCanvas.SetActive(value: false);
		}
		if (gameState == GAMESTATES.VICTORY)
		{
			instance.gameCanvas.SetActive(value: false);
			instance.menuCanvas.SetActive(value: false);
			instance.gameOverCanvas.SetActive(value: false);
			instance.victoryCanvas.SetActive(value: true);
		}
	}

	public void UpdateText()
	{
		instance.txtBrickCount.text = "Target:" + SquareScript.squares.Count + "/" + targetCount;
		instance.txtLives.text = "Lives:" + lives;
		instance.txtScore.text = "Score:" + score;
	}

	public void StartGame()
	{
		SetGameState(GAMESTATES.GAME);
		SetupGameObjects();
		SetLevel(1);
	}

	private void Update()
	{
		if (gameState == GAMESTATES.GAME)
		{
			UpdateText();
		}
	}

	private void SetupGameObjects()
	{
		Object.Instantiate(Resources.Load("Paddle/Paddle"));
		Object.Instantiate(Resources.Load("Ball/Ball"));
		Object.Instantiate(Resources.Load("EdgeColliders/LeftEdgeCollider"));
		Object.Instantiate(Resources.Load("EdgeColliders/RightEdgeCollider"));
		Object.Instantiate(Resources.Load("EdgeColliders/TopCollider"));
		Object.Instantiate(Resources.Load("EdgeColliders/DeathZone"));
		paddle = GameObject.Find("Paddle(Clone)");
	}

	public void SetScore(int score)
	{
		this.score = score;
	}

	public void SetLives(int lives)
	{
		this.lives = lives;
	}

	public void UpdateScore(int score)
	{
		this.score += score;
	}

	public void IncrementLevel()
	{
		level++;
		if (level == 7)
		{
			SetGameState(GAMESTATES.VICTORY);
		}
		else
		{
			SetLevel(level);
		}
	}

	public void Quit()
	{
		Application.Quit();
	}
}
