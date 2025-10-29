using UnityEngine;

[CreateAssetMenu(fileName = "ProjectableShot", menuName = "Weapons/Shots/Projectile")]
public class ProjectileShotSO : ShotObjectSO
{

	[SerializeField] private ProjectileBase shotPrefab;
	[SerializeField] private ProjectileBase chargeShotPrefab;

	public override void Shot(Vector3 shotPoint, Vector3 direction)
	{
		ProjectileBase shotInstance = Instantiate(shotPrefab, shotPoint, Quaternion.identity);
		shotInstance.SetMovement(direction);
	}

	public void ChangeShot(Vector3 shotPoint, Vector3 direction) 
	{
		ProjectileBase shotInstance = Instantiate(chargeShotPrefab, shotPoint, Quaternion.identity);
		shotInstance.SetMovement(direction);
	}

	public bool HasChargeShot() => chargeShotPrefab != null;
}
