using System;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileWeapon : Weapon
{
	
	[SerializeField] private ProjectileBase shootPrefab;
	[SerializeField] private ProjectileBase chargeShootPrefab;

	[SerializeField] private float chargeTime;
	[SerializeField] private bool canCharge;

	public UnityEvent OnStartChargeWeapon;
	public UnityEvent<float> OnChargeWeapon;
	public UnityEvent OnFullyChageWeapon;
	public UnityEvent OnRelaseChargeWeapon;
	public UnityEvent OnCancelChargeWeapon;

	private bool _isCharging;
	private bool _isFullyCharged;
	private float _chargeTimer;
	private bool _chargeThreshold;


	private void OnEnable()
	{
		ResetChargeShoot();
	}

	private void OnDisable()
	{
		ResetChargeShoot();
	}

	private void ResetChargeShoot()
	{
		_isCharging = false;
		_chargeTimer = 0;
		OnChargeWeapon?.Invoke(0);
	}

	private void Update()
	{
		if (!_isCharging)
		{
			if (_chargeTimer > 0) _chargeTimer = 0;
			return;
		}

		ChargeWeapon();
	}

	private void ChargeWeapon() 
	{
		if (_chargeTimer >= chargeTime)
			return;

		_chargeTimer += Time.deltaTime;

		if (_chargeTimer > chargeTime * 0.3f && !_chargeThreshold) 
		{
			OnStartChargeWeapon?.Invoke();
			_chargeThreshold = true;
		}
			

		if (_chargeTimer >= chargeTime) 
		{
			_chargeTimer = chargeTime;
			_isFullyCharged = true;
			OnFullyChageWeapon?.Invoke();
		}

		OnChargeWeapon?.Invoke(_chargeTimer / chargeTime);
	}

	public override void StartShoot()
	{
		if (!base.CanShoot())
			return;

		base.SetNextShootTime();
		
		ProjectileBase shotInstance = Instantiate(shootPrefab, base.ShootPoint.position, Quaternion.identity);
		shotInstance.SetMovement(base.ShootPoint.forward);
		shotInstance.SetDamage(base.Data.Damage);

		_isFullyCharged = false;

		if (!canCharge || chargeShootPrefab == null)
			return;

		_isCharging = true;
		_chargeTimer = 0;
	}

	public override void EndShoot()
	{
		_chargeTimer = 0;
		_isCharging = false;
		_chargeThreshold = false;

		OnCancelChargeWeapon?.Invoke();

		if (!_isFullyCharged) return;

		ProjectileBase shotInstance = Instantiate(chargeShootPrefab, base.ShootPoint.position, Quaternion.identity);
		shotInstance.SetMovement(base.ShootPoint.forward);
		shotInstance.SetDamage(base.Data.Damage * 2);

		_isFullyCharged = false;

		OnChargeWeapon?.Invoke(0);
		OnRelaseChargeWeapon?.Invoke();
	}

	public void TriggerChargeShoot()
	{
		_isCharging = true;
		_chargeTimer = 0;
	}


}
