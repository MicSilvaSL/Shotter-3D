using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	[SerializeField] private InputManager input;
	[SerializeField] private Transform weaponShotPoint;
	[SerializeField] private float maxRange = 5;

	public static event Action<Weapon> OnChangeWeapon;

	private Weapon[] _weaponsList;
	private Camera _mainCam;
	private Weapon _currentWeapon;
	private Ray _weaponAim;


	private int _id = 0;
	private int _layerTraget = 0;

	private void Awake()
	{
		_weaponsList = this.GetComponents<Weapon>();
	}

	private void OnEnable()
	{
		input.ShotStarted += OnShotStart;
		input.ShotCanceled += OnShotCancel;
		input.ChangeWeapon += ChangeCurrentWeapon;
	}

	private void OnDisable()
	{
		input.ShotStarted -= OnShotStart;
		input.ShotCanceled -= OnShotCancel;
		input.ChangeWeapon -= ChangeCurrentWeapon;
	}

	private void Start()
	{
		SetCurrentWeapon();

		_mainCam = Camera.main;
		_layerTraget = ~(1 << this.gameObject.layer);
	}

	private void FixedUpdate()
	{
		if (_currentWeapon == null) return;

		Ray centerAnim = _mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		_weaponAim = new Ray(weaponShotPoint.position, (centerAnim.GetPoint(maxRange) - weaponShotPoint.position).normalized);

		Debug.DrawRay(_weaponAim.origin, _weaponAim.direction * maxRange, Color.blue);

	}
	public void ChangeCurrentWeapon(int amount = 1)
	{
		int nextId = _id + (int)Mathf.Sign(amount);

		nextId = SetValidId(nextId);

		_id = CheckValidSlot(nextId);
		
		SetCurrentWeapon();

		OnChangeWeapon?.Invoke(_currentWeapon);

	}

	public void SetCurrentWeapon() 
	{
		if (_weaponsList.Length <= 0) return;

		_currentWeapon = _weaponsList[_id];
		
		
	}

	private void OnShotStart()
	{
		_currentWeapon.StartShoot(_weaponAim);
	}

	private void OnShotCancel()
	{
		_currentWeapon.EndShoot(_weaponAim);
	}

	private int SetValidId(int id)
	{
		if (id > _weaponsList.Length - 1)
			id = 0;
		else if (id < 0)
			id = _weaponsList.Length - 1;

		return id;
	}
	private int CheckValidSlot(int id)
	{
		id = SetValidId(id);

		if (_weaponsList[id] == null)
			return CheckValidSlot(id + (int)Mathf.Sign(id - _id));
		else
			return id;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(_weaponAim.origin, _weaponAim.direction * maxRange);

	}

}

