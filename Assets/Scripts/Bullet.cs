using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
		// if(other.gameObject.CompareTag("Destroyable"))
		// {
		// 	Destroy(other.gameObject);
		// }
		if(other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
		{
			Destroy(other.gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		Debug.LogError("OnCollisionEnter");
	}
}
