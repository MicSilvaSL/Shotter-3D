using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName ="Weapon Data")]
public class WeaponSO : ScriptableObject
{
	[field: SerializeField] public Sprite Icon { get; private set; }
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public float FireRate { get; private set; } = 0.1f;

	[field: SerializeField] public float Damage { get; private set; } = 10;


	protected void OnValidate()
	{
		if (FireRate < 0)
			FireRate = 0.1f;
		else
			FireRate = Mathf.Abs(FireRate);

		if (Damage < 0)
			Damage = 1;
		else
			Damage = Mathf.Abs(Damage);
	}

	
    
}
