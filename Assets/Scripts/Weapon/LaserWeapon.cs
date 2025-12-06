using UnityEngine;
using UnityEngine.Events;

public class LaserWeapon : Weapon
{
	private bool _isOnUse;

	public UnityEvent OnTurnOnLaser;
	public UnityEvent OnTurnOffLaser;
	public UnityEvent<Vector3, Vector3> OnRayUse;

	public override void StartShoot()
	{
		_isOnUse = true;
		OnTurnOnLaser.Invoke();
	}

	public override void EndShoot()
	{
		_isOnUse = false;
		OnTurnOffLaser.Invoke();
	}

	private void Update()
	{
		if (!_isOnUse)
			return;
	}

	private void FixedUpdate()
	{
		if (!_isOnUse)
			return;

		Debug.DrawRay(base.WeaponAim.Aim.origin, base.WeaponAim.Aim.direction * base.WeaponAim.MaxRange, Color.yellow);
		
		if (Physics.Raycast(base.WeaponAim.Aim, out RaycastHit hitInfo, base.WeaponAim.MaxRange, base.WeaponAim.TargetLayer))
		{
			OnRayUse.Invoke(base.WeaponAim.Aim.origin, hitInfo.point);
			
			if (base.CanShoot())
			{
				if (hitInfo.collider.TryGetComponent(out Damageble damageble))
				{
					damageble.TakeDamage(base.Data.Damage);
				}

				base.SetNextShootTime();
			}
		}
		else
		{
			OnRayUse.Invoke(base.WeaponAim.Aim.origin, base.WeaponAim.Aim.origin + base.WeaponAim.Aim.direction * base.WeaponAim.MaxRange);
		}



		
		
	}

}
