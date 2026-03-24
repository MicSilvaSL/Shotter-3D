using UnityEngine;
using UnityEngine.Events;


public class ShootOnRange : MonoBehaviour
{
	[SerializeField] private string targetTag;
	[SerializeField] private ProjectileWeapon weaponProjectile;
	[SerializeField] private Transform centerPoint;

	private Transform _target = null;

	private void Update()
	{
		if (_target == null) return;

		centerPoint.forward = (_target.position - centerPoint.position).normalized;


		if (weaponProjectile.CanShoot())
		{
			weaponProjectile.StartShoot();
			weaponProjectile.EndShoot();
		}
	}
    

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(targetTag))
		{
			_target = other.transform;
		}
			

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag(targetTag))
		{
			_target = null;
		}
			

	}
}
