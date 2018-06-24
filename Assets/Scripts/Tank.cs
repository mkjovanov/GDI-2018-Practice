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
	[SerializeField]
	private Camera _mainCamera;
	[SerializeField]
	private int _startingHealth;

	private Rigidbody _myRigidBody;
	private bool _isMovingForward;
	private bool _isMovingBackward;
	private float _timeToReload;
	private float _cameraSpeed = 0.05f;
	private bool _isPlayerMoving;
	
	private int _health;
	public int Health
	{
		get { return _health; }
		private set
		{
			Debug.Log("Health.set - old value: " + Health + ", new value: " + value);
			_health = value;
			GameManager.Instance.OnHealthChange(Health);
		}
	}

	public void Awake()
	{
		_myRigidBody = GetComponent<Rigidbody>();
		_health = _startingHealth;
	}

	private void Update()
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
		//PlayerCameraFollow();
	}

	private void FixedUpdate()
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

	public void OnHit()
	{
		Health--;
		if(_health == 0)
		{
			Destroy(gameObject);
		}
	}

	private void Shoot()
	{
		_timeToReload = _reloadTime;
		var bullet = Instantiate(_bulletPrefab);
		bullet.transform.position = _fireAtTransform.position;
		bullet.transform.rotation = transform.rotation;
		bullet.GetComponent<Rigidbody>().AddForce(_shootingForce * transform.forward);
	}

	private void PlayerCameraFollow()
	{
		var interpolation = _cameraSpeed;// * Time.deltaTime;

		var position = _mainCamera.transform.position;
		position.z = Mathf.Lerp(_mainCamera.transform.position.z, transform.position.z, interpolation);
		position.x = Mathf.Lerp(_mainCamera.transform.position.x, transform.position.x, interpolation);

		_mainCamera.transform.position = position;

		Debug.Log("--------------------------------");
		Debug.Log("Camera: x - " + _mainCamera.transform.position.x + ", z - " + _mainCamera.transform.position.z);
		Debug.Log("Player: x - " + transform.position.x + ", z - " + transform.position.z);
		Debug.Log("--------------------------------");
	}
}
