using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour 
{
	[SerializeField]
	private float _rotateSpeed;
	[SerializeField]
	private float _reloadTime;
    [SerializeField]
    private Transform _fireAt;
	[SerializeField]
    private float _shootingForce = 1500;
	[SerializeField]
	private LayerMask _blockingLayers;
	
	private float _timeToReload = 1f;
	private Tank _tank;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		_tank = FindObjectOfType<Tank>();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		var towerToTank = _tank.transform.position - transform.position;
		towerToTank.y = 0f;
		var rotation = Quaternion.LookRotation(towerToTank);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);

		if(_timeToReload > 0)
        {
            _timeToReload -= Time.deltaTime;
        }

		Debug.DrawRay(_fireAt.position, _fireAt.forward * 1000, Color.red, 1f);
		if(_timeToReload <= 0f)
		{
			RaycastHit hit;
			if(Physics.Raycast(_fireAt.position, _fireAt.forward, out hit, 1000f))
			{
				if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Tank"))
				{
					Shoot();
					_reloadTime = _timeToReload;
				}
			}
		}
	}

	private void Shoot()
    {
        _timeToReload = _reloadTime;
		var bulletPrefab = Resources.Load("Prefabs/CompleteShell") as GameObject;
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = _fireAt.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody>().AddForce(_shootingForce * transform.forward);
    }
}
