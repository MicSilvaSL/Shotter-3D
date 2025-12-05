using UnityEngine;

public class LaserWeapon : Weapon
{
	private bool _isOnUse;

	public override void EndShoot() => _isOnUse = false;

	public override void StartShoot() => _isOnUse = true;

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

		if (base.CanShoot()) 
		{
			if (Physics.Raycast(base.WeaponAim.Aim,out RaycastHit hitInfo, base.WeaponAim.MaxRange, base.WeaponAim.TargetLayer))
			{
				if (hitInfo.collider.TryGetComponent(out Damageble damageble))
				{
					damageble.TakeDamage(base.Data.Damage);
				}	
			}
			
			base.SetNextShootTime();
		}
		
	}

}
