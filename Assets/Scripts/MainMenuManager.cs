using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour 
{
	public Button StartButton;
	public Button QuitButton;

	private void Awake()
	{
		StartButton.onClick.AddListener(OnStartButtonClick);
		QuitButton.onClick.AddListener(OnQuitButtonClick);
	}

	private void OnStartButtonClick()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void OnQuitButtonClick()
	{
		Application.Quit();
	}

	private void OnMouseUpAsButton()
	{
		
	}
}
