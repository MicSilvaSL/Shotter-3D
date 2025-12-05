using UnityEngine;

[RequireComponent(typeof(WeaponAim))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSO data;

	protected WeaponAim WeaponAim;
	protected float NextShootTime;

	protected void Awake()
	{
		WeaponAim = GetComponent<WeaponAim>();
	}


	public WeaponSO Data => data;

	public abstract void StartShoot();

	public abstract void EndShoot();

	protected bool CanShoot() => NextShootTime <= Time.time;

	protected void SetNextShootTime() => NextShootTime = Time.time + data.FireRate;
}
