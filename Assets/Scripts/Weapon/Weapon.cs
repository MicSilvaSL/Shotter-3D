using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSO data;

	public WeaponSO Data => data;

	public virtual void StartShoot(Ray aim) 
	{
	}

	public virtual void EndShoot(Ray aim) 
	{
		
	}
}
