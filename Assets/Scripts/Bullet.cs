using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	private bool _exploding;
	private float _timeToDestroy;

	private void Update()
	{
		if (_exploding)
		{
			_timeToDestroy -= Time.deltaTime;
			if (_timeToDestroy <= 0f)
			{
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
		if(	other.gameObject.layer == LayerMask.NameToLayer("Buildings") ||
			other.gameObject.layer == LayerMask.NameToLayer("Destroyable"))
		{
			Destroy(other.gameObject);
		}

		if(_exploding)
		{
			return;
		}

		if (other.gameObject.CompareTag("Destroyable"))
		{
			var tank = other.gameObject.GetComponent<Tank>();
			if(tank != null)
			{
				tank.OnHit();
			}
			else
			{
				other.gameObject.SetActive(false);
			}
		}

		// fire explosion and explode
		//GetComponentInChildren<ParticleSystem>().Play();
		//GetComponent<Rigidbody>().velocity = Vector3.zero;
		//_timeToDestroy = 0.5f;
		//_exploding = true;
	}
}
