using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameStateType GameState { get; set; }
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<GameManager>();
			}
			return _instance;
		}
	}
	public enum GameStateType
	{
		Playing,
		Paused,
		EndGame,
	}

	public Action<bool> OnGameEnds;
	public Action<int> OnHealthChange;

	private static GameManager _instance;
	private Tank _tank;
	private List<Tower> _towers;

	private void Start()
	{
		_tank = FindObjectOfType<Tank>();
		_towers = FindObjectsOfType<Tower>().ToList();
		ResumeGame();
	}

	private void Update()
	{
		if (GameState == GameStateType.Playing && IsEnd())
		{
			Time.timeScale = 0f;
			GameState = GameStateType.EndGame;

			if (OnGameEnds != null)
			{
				var playerHasWon = _tank.Health > 0;
				OnGameEnds(playerHasWon);
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			switch (GameState)
			{
				case GameStateType.Playing:
					PauseGame();
					break;
				case GameStateType.Paused:
					ResumeGame();
					break;
				case GameStateType.EndGame:
					ReloadGame();
					break;
				default: break;
			}
		}
	}


	private void PauseGame()
	{
		Time.timeScale = 0f;
		GameState = GameStateType.Paused;
	}

	private void ResumeGame()
	{
		Time.timeScale = 1f;
		GameState = GameStateType.Playing;
	}

	private void ReloadGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private bool IsEnd()
	{
		return
			_tank.Health == 0 ||
			_towers.All(tower => !tower.gameObject.activeInHierarchy);
	}
}
