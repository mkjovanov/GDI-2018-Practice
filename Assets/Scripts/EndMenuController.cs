using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuController : MonoBehaviour 
{
	[SerializeField]
	private Text _endGameText;

	private void Awake()
	{
		gameObject.SetActive(false);
		GameManager.Instance.OnGameEnds += OnGameEnds;
	}

	private void OnGameEnds(bool playerWon)
	{
		gameObject.SetActive(true);
		_endGameText.text = playerWon ? "YOU WON!" : "YOU DIED!";
	}
}
