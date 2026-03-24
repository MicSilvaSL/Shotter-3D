using UnityEngine;
using UnityEngine.Events;


public class LaserWeapon : Weapon
{
	[SerializeField] private float maxRange = 10;
	[SerializeField] private LayerMask _layerTarget;

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

		Ray laserAim = new Ray(ShootPoint.position, ShootPoint.forward);

		Debug.DrawRay(laserAim.origin, laserAim.direction * maxRange, Color.yellow);
		
		if (Physics.Raycast(laserAim, out RaycastHit hitInfo, maxRange, _layerTarget))
		{
			OnRayUse.Invoke(laserAim.origin, hitInfo.point);
			
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
			OnRayUse.Invoke(laserAim.origin, laserAim.origin + laserAim.direction * maxRange);
		}



		
		
	}

}
