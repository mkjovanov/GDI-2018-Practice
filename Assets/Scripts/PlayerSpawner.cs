using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	[SerializeField]
	private List<Vector3> _spawningPositions;

	private void Awake()
	{
		transform.position = _spawningPositions[Random.Range(0, _spawningPositions.Count)];
	}
}
