using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour 
{
	[SerializeField]
	private Tank _tank;
	[SerializeField]
	private List<Image> _healthImages;
	[SerializeField]
	private Color _aliveColor;
	[SerializeField]
	private Color _deadColor;

	private void Awake()
	{
		GameManager.Instance.OnHealthChange += OnHealthChange;	
	}
	
	private void OnHealthChange(int health)
	{
		Debug.Log("OnHealthChange");
		for (int i = 0; i < _healthImages.Count; i++)
		{
			_healthImages[i].color = i < health ? _aliveColor : _deadColor;
		}
	}
}
