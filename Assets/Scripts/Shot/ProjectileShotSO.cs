using UnityEngine;

[CreateAssetMenu(fileName = "ProjectableShot", menuName = "Weapons/Shots/Projectile")]
public class ProjectileShotSO : ShotObjectSO
{

	[SerializeField] private ProjectileBase shotPrefab;
	[SerializeField] private ProjectileBase chargeShotPrefab;

	public override void Shot(Vector3 shotPoint, Vector3 direction, string[] damageTags)
	{
		ProjectileBase shotInstance = Instantiate(shotPrefab, shotPoint, Quaternion.identity);
		shotInstance.SetMovement(direction);
		shotInstance.SetTargets(damageTags);
	}

	public void ChangeShot(Vector3 shotPoint, Vector3 direction, string[] damageTags)  
	{
		ProjectileBase shotInstance = Instantiate(chargeShotPrefab, shotPoint, Quaternion.identity);
		shotInstance.SetMovement(direction);
		shotInstance.SetTargets(damageTags);
	}

	public bool HasChargeShot() => chargeShotPrefab != null;
}
