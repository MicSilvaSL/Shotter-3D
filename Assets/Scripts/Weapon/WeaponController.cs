using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	[SerializeField] private InputManager input;

	public static event Action<Weapon> OnChangeWeapon;

	private Weapon[] _weaponsList;
	
	private Weapon _currentWeapon;


	private int _id = 0;

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
		
	}

	private void FixedUpdate()
	{
		if (_currentWeapon == null) return;


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
		_currentWeapon.StartShoot();
	}

	private void OnShotCancel()
	{
		_currentWeapon.EndShoot();
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

}

