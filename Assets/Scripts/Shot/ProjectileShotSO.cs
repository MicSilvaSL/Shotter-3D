using UnityEngine;

[CreateAssetMenu(fileName = "ProjectableShot", menuName = "Weapons/Shots/Projectile")]
public class ProjectileShotSO : ShotObjectSO
{
	[field: SerializeField] public float FireRate { get; private set; } = 0.1f;

	[SerializeField] private GameObject ShotInstance;

	private void OnValidate()
	{
		if (FireRate < 0)
			FireRate = 0.1f;
		else
			FireRate = Mathf.Abs(FireRate);
	}

	public override void Shot()
	{
		
	}
}
