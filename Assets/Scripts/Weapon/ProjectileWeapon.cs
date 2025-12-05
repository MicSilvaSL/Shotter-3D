using System;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
	
	[SerializeField] private ProjectileBase shootPrefab;
	[SerializeField] private ProjectileBase chargeShootPrefab;

	[SerializeField] private float chargeTime;
	[SerializeField] private bool canCharge;

	public static event Action<float> OnChargeWeapon;
	public static event Action OnFullyChageWeapon;

	private bool _isCharging;
	private bool _isFullyCharged;
	private float _chargeTimer;
	
	

	private void OnDisable()
	{
		_isCharging = false;
		_chargeTimer = 0;
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
		
		ProjectileBase shotInstance = Instantiate(shootPrefab, base.WeaponAim.Aim.origin, Quaternion.identity);
		shotInstance.SetMovement(base.WeaponAim.Aim.direction);
		shotInstance.SetDamage(base.Data.Damage);

		_isFullyCharged = false;

		if (!canCharge || chargeShootPrefab == null)
			return;

		_isCharging = true;
		_chargeTimer = 0;
	}

	public override void EndShoot()
	{
		if (!_isFullyCharged) return;

		ProjectileBase shotInstance = Instantiate(chargeShootPrefab, base.WeaponAim.Aim.origin, Quaternion.identity);
		shotInstance.SetMovement(base.WeaponAim.Aim.direction);
		shotInstance.SetDamage(base.Data.Damage * 2);

		_chargeTimer = 0;
		_isCharging = false;
		_isFullyCharged = false;

		OnChargeWeapon?.Invoke(0);
	}


}
