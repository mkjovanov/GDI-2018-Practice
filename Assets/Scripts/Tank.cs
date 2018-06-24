using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
	[SerializeField]
	private float _moveSpeed = 20f;
	[SerializeField]
	private float _rotateSpeed = 100f;
	[SerializeField]
	private GameObject _bulletPrefab;
	[SerializeField]
	private Transform _fireAtTransform;
	[SerializeField]
	private float _shootingForce = 1500;
	[SerializeField]
	private float _reloadTime = 1f;

	private Rigidbody _myRigidBody;

	private bool _isMovingForward;
	private bool _isMovingBackward;
	private float _timeToReload;

	public void Awake()
	{
		_myRigidBody = GetComponent<Rigidbody>();
	}

	void Start()
	{
		Debug.Log("Start");
	}

	void Update()
	{
		_isMovingForward = Input.GetKey(KeyCode.W);
		_isMovingBackward = Input.GetKey(KeyCode.S);

		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.up, -_rotateSpeed * Time.deltaTime);
		}

		if (_timeToReload > 0)
		{
			_timeToReload -= Time.deltaTime;
		}
		if (Input.GetKeyDown(KeyCode.Space) && _timeToReload <= 0)
		{
			Shoot();
		}
	}

	void FixedUpdate()
	{
		if (_isMovingForward)
		{
			_myRigidBody.AddForce(transform.forward * _moveSpeed * Time.deltaTime);
		}
		if (_isMovingBackward)
		{
			_myRigidBody.AddForce(-transform.forward * _moveSpeed * Time.deltaTime);
		}
	}

	void OnDestroy()
	{
		Debug.Log("Destroy");
	}

	private void Shoot()
	{
		_timeToReload = _reloadTime;
		var bullet = Instantiate(_bulletPrefab);
		bullet.transform.position = _fireAtTransform.position;
		bullet.transform.rotation = transform.rotation;
		bullet.GetComponent<Rigidbody>().AddForce(_shootingForce * transform.forward);
	}
}
