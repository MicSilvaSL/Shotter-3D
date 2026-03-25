using UnityEngine;
using System.Linq;

public class ShootOnRange : MonoBehaviour
{
	[SerializeField] private string targetTag;
	[SerializeField] private LayerMask layerTarget;
	[SerializeField] private float range;
	[SerializeField] private ProjectileWeapon weaponProjectile;
	[SerializeField] private Transform centerPoint;

	private Collider[] _targets = new Collider [5];
	private Collider _closetTarget;

	private void Update()
	{
		if (_targets.Length <= 0) return;

		Collider nearTarget = GetNearTarget();
		if (!nearTarget.Equals(_closetTarget))
			_closetTarget = nearTarget;

		if (_closetTarget == null) return;

		Vector3 distance = _closetTarget.transform.position - centerPoint.position;
		centerPoint.forward = distance.normalized;


		if (weaponProjectile.CanShoot())
		{
			weaponProjectile.StartShoot();
			weaponProjectile.EndShoot();
		}
	}

	private void FixedUpdate()
	{
		//TODO: NonAlloc
		_targets = Physics.OverlapSphere(centerPoint.position, range, layerTarget);
	}

	private Collider GetNearTarget()
	{
		if (_targets.Length <= 0) return null;
		if (_targets.Length == 1) return _targets[0];

		return _targets.OrderBy(t => Vector3.Distance(centerPoint.position, t.transform.position)).ToArray()[0];
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(centerPoint.position, range);
	}
}
