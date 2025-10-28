using UnityEngine;

[CreateAssetMenu(fileName = "LaserShot", menuName = "Weapons/Shots/Laser")]
public class LaserShotSO : ShotObjectSO
{
	[field: SerializeField] public float TickRate { get; private set; } = 0.5f;


	private void OnValidate()
	{
		if (TickRate < 0.01f)
			TickRate = 0.01f;
		else
			TickRate = Mathf.Abs(TickRate);
	}

	public override void Shot()
	{
		
	}
}
