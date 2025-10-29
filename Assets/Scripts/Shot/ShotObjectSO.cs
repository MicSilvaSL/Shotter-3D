using UnityEngine;

public abstract class ShotObjectSO : ScriptableObject
{
	[field: SerializeField] public float FireRate { get; private set; } = 0.1f;
	[field: SerializeField] public Sprite Icon { get; private set; }
	protected void OnValidate()
	{
		if (FireRate < 0)
			FireRate = 0.1f;
		else
			FireRate = Mathf.Abs(FireRate);
	}

	public abstract void Shot(Vector3 shotPoint, Vector3 direction);
    
}
