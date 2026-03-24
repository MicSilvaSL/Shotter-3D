using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSO data;
	[SerializeField] protected Transform ShootPoint;
	protected float NextShootTime;

	public WeaponSO Data => data;

	public abstract void StartShoot();

	public abstract void EndShoot();

	public bool CanShoot() => NextShootTime <= Time.time;

	protected void SetNextShootTime() => NextShootTime = Time.time + data.FireRate;
}
